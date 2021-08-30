using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Saving
{
    public class SavingSystem : MonoBehaviour, ISavingSystem
    {
        public void Save(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            Debug.Log($"Saving to: {path}");
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                Transform playerTransform = GetPlayerTransform();
                byte[] buffer = SerializeVector(playerTransform.position);
                
                stream.Write(buffer, 0, buffer.Length);
            }
        }

       

        public void Load(string saveFile)
        {
            Debug.Log($"Loading from: {GetPathFromSaveFile(saveFile)}");
            string path = GetPathFromSaveFile(saveFile);
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);

                PlayerManager.Instance.Player.position = DeserializeVector(buffer);
            }
        }
        private Transform GetPlayerTransform()
        {
            return PlayerManager.Instance.Player;
        }

        private byte[] SerializeVector(Vector3 vector)
        {
            byte[] vectorBytes = new byte[3 * 4];
            BitConverter.GetBytes(vector.x).CopyTo(vectorBytes, 0);
            BitConverter.GetBytes(vector.y).CopyTo(vectorBytes, 4);
            BitConverter.GetBytes(vector.z).CopyTo(vectorBytes, 8);

            return vectorBytes;
        }

        private Vector3 DeserializeVector(byte[] buffer)
        {
            Vector3 result = new Vector3();
            result.x = BitConverter.ToSingle(buffer, 0);
            result.y = BitConverter.ToSingle(buffer, 4);
            result.z = BitConverter.ToSingle(buffer, 8);
            
            return result;
        }
        
        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}


