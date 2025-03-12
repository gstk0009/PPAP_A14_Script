using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public enum State
{
    Idle, // 직진으로 꽂아버리기
    Random, // 랜덤 방향으로 발사
    Chasing, // 캐릭터 방향으로 발사
    Crashing // 충돌해서 터지기 직전
}

public class Vehicle : MonoBehaviour
{
    private VehicleEffect effect;

    [Header("Vehicle")]
    [SerializeField] private float IdlePower;
    [SerializeField] private float RandomPower;
    [SerializeField] private float ChasePower;

    [SerializeField] private float RotRandomSpeed = 2f; // Random 선형 보간 매개 변수
    [SerializeField] private float RotChaseSpeed = 20f; // Chasing 선형 보간 매개 변수

    [SerializeField] private float fov;
    [SerializeField] private float lifeTime = 40f;

    private float resetObjectDelayTime = 1f;
    private float resetLifeTime = 40f;
    public LayerMask collisionLayerMask;

    private Vector3 targetV;
    private Quaternion targetRot;
    private Vector3 curV;
    private Quaternion curRot;
    private Vector3 dirV;

    private Vector3 resetPosition;
    private Quaternion resetRotation;

    private bool isFirstBreak = true;
    private ParticleSystem spark;

    [Range(0.0f, 1.0f)]
    public float percent = 0.5f; // Chasing - Random 확률

    public State curState = State.Idle;
    public Player player;
    private bool detect = false;
    private float detectDistance = 15f;
    private Rigidbody  vehicleRB;

    public ParticleSystem[] ps;
    private void Awake()
    {
        effect = GetComponent<VehicleEffect>();
        vehicleRB = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        firstGo();
        player = CharacterManager.Instance.Player;
    }

    private void FixedUpdate()
    {
        if (Time.timeScale != 0f)
        {
            if (lifeTime > 0)
                lifeTime -= Time.deltaTime;
            else
                curState = State.Crashing;

            switch (curState)
            {
                case State.Idle: firstGo(); break;
                case State.Random: VehicleMove(); CheckUpdate(); break;
                case State.Chasing: SetTarget(); VehicleMove(); 
                    if(!detect)
                        CheckUpdate(); break;
                case State.Crashing: BeforeCrashing(); break;
            }
        }
    }

    private void CheckUpdate()
    {
        if (!detect && CheckForward())
        {
            effect.SkidMarkSound();
            detect = true;
        }
    }

    bool CheckForward()
    {
        Ray[] rays = new Ray[6]
        {
            new Ray(transform.position + (transform.forward*3f) + (transform.right*1f) + (transform.up * 0.5f),Vector3.right),
            new Ray(transform.position + (transform.forward*3f) +(transform.right*-1f) + (transform.up * 0.5f),Vector3.right),
            new Ray(transform.position + (transform.forward*3f) + (transform.up * 1.5f),Vector3.right),
            new Ray(transform.position + (transform.forward*3f) + (transform.up * 1.5f),Vector3.right),
            new Ray(transform.position + (transform.forward*3f) + (transform.right*1f) + (transform.up * 1.5f),Vector3.right),
            new Ray(transform.position + (transform.forward*3f) +(transform.right*-1f) + (transform.up * 1.5f),Vector3.right),
        };

        for (int i = 0; i < 6; ++i)
        {
            if (Physics.Raycast(rays[i], detectDistance, collisionLayerMask))
                return true;
        }
        return false;
    }

    void BeforeCrashing() { Invoke("releaseVehicle", 5f); }

    private void firstGo()
    {
        vehicleRB.AddForce(transform.forward * IdlePower * Time.deltaTime, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other) // Random 또는 Chasing 상태로 이동
    {
        if (curState == State.Idle && other.CompareTag("Player"))
        {
            effect.HornSound();
            if (Random.Range(0f, 1f) < percent)
            {
                // y축 기준, -fov' ~ fov' 회전
                float randomAngle = Random.Range(-fov, fov);

                // 쿼터니언으로 변환 후, 목표 진행 방향을 계산
                targetV = Quaternion.Euler(0f, randomAngle, 0f) * transform.forward;
                targetV = targetV.normalized;

                // 목표 회전 방향을  계산
                targetRot = Quaternion.LookRotation(targetV, transform.up);
                curState = State.Random;
            }
            else
            {
                curState = State.Chasing;
            }
        }
    }

    private void SetTarget()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 carPos = transform.position;

        targetV = (playerPos - carPos).normalized;
        dirV = targetV - curV;
        targetRot = Quaternion.LookRotation(targetV, transform.up);
    }

    private void VehicleMove()
    {
        curV = transform.forward;
        curRot = transform.rotation;

        Vector3 v = curState == State.Random ? targetV : dirV;

        float force = curState == State.Random ? RandomPower : ChasePower;
        float rotSpeed = curState == State.Random ? RotRandomSpeed : RotChaseSpeed;

        if (curV != targetV)
            curV = Vector3.Lerp(curV, v, rotSpeed * Time.deltaTime);

        if (curRot != targetRot)
            curRot = Quaternion.Lerp(curRot, targetRot, rotSpeed * Time.deltaTime);


        curV.y = 0.0f;
        transform.rotation = curRot;

        vehicleRB.AddForce(curV * force * Time.deltaTime, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision) // 충돌 시
    {
        if (collision.gameObject.layer == 6)
        {
            effect.CollisionSound();
            Rigidbody _rb = collision.gameObject.GetComponent<Rigidbody>();
            curV.y = 0;
            _rb.AddForce(curV * vehicleRB.velocity.magnitude, ForceMode.VelocityChange);
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, contact.normal);

            if (isFirstBreak)
            {
                isFirstBreak = false;
                int i = Random.Range(0, ps.Length);
                spark = Instantiate(ps[i], contact.point + (-contact.normal * 0.05f), rot);
                spark.transform.SetParent(transform);
            }

            else
                spark.gameObject.SetActive(true);

            curState = State.Crashing;
        }
        else if (collision.gameObject.layer == 12 || collision.gameObject.layer == 13 || collision.gameObject.layer == 14)
        {
            effect.ExplosionSound();
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, contact.normal);

            if (isFirstBreak)
            {
                isFirstBreak = false;
                int i = Random.Range(0, ps.Length);
                spark = Instantiate(ps[i], contact.point + (-contact.normal * 0.05f), rot);
                spark.transform.SetParent(transform);
            }
            else
                spark.gameObject.SetActive(true);

            curState = State.Crashing;
        }
    }

    public void SetReset(Vector3 position, Quaternion rotation)
    {
        resetPosition = position;
        resetRotation = rotation;
    }

    // BeforeCrashing 함수에서 Invoke
    private void releaseVehicle()
    {
        gameObject.SetActive(false);

        Invoke("ResetCar", 1f);
    }

    // releaseVehicle 함수에서 Invoke
    public void ResetCar()
    {
        if (spark != null)
        {
            spark.gameObject.SetActive(false);
        }

        transform.position = resetPosition;
        transform.rotation = resetRotation;
        lifeTime = resetLifeTime;
        effect.StopSound();
        detect = false;
        gameObject.SetActive(true);
        curState = State.Idle;
        vehicleRB.velocity = Vector3.zero;
        firstGo();
    }
}