﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NitorInc.Utility;


namespace NitorInc.YuukaWater {

    public class YuukaWaterWaterLauncher : MonoBehaviour {

        public GameObject[] waterDrops;
        public float spawnRadius = 0.15f;
        public Transform waterSpawnPoint;
        float direction = 1.0f;
        public float waterInterval = 0.1f;
        public float dropVelMin = 0.15f;
        public float dropVelMax = 2.0f;
        float yuukaVel = 0.0f;

        Timer spawnTimer;

        // Use this for initialization
        void Start() {
            spawnTimer = TimerManager.NewTimer(waterInterval, Spawn, 0, false, false);
            spawnTimer.Start();

            YuukaWaterController.OnVictory += TurnOff;
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Q)) {
                TurnOff();
            }
            if (Input.GetKeyDown(KeyCode.E)) {
                TurnOn();
            }
        }

        void Spawn() {
            Vector3 fuzz = Random.insideUnitCircle * spawnRadius;
            Vector3 pos = waterSpawnPoint.position + fuzz;
            var drop = Instantiate(waterDrops[Random.Range(0, waterDrops.Length)], pos, Quaternion.identity).GetComponent<YuukaWaterWaterdrop>();
            Vector2 vel = new Vector2(Random.Range(dropVelMin, dropVelMax), 0.0f) * direction;
            vel.x += yuukaVel;
            drop.SetInitialForce(vel);
            spawnTimer.Restart();
        }

        public void TurnOn() {
            spawnTimer.Restart();
        }
        public void TurnOff() {
            spawnTimer.Stop();
        }

        public void UpdateYuukaVel (float vel) {
            yuukaVel = vel;
        }
    }
}
