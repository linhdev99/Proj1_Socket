using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
namespace BKSpeed
{
    public class ClientTransform
    {
        [JsonProperty]
        public List<float> position { get; set; }
        [JsonProperty]
        public List<float> rotation { get; set; }
        [JsonProperty]
        public List<float> scale { get; set; }

        public ClientTransform(List<float> position, List<float> rotation, List<float> scale)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }

        public ClientTransform(Transform trans = null)
        {
            if (trans == null) return;
            this.position = new List<float> {
                BaseMath.Round(trans.position.x, 5),
                BaseMath.Round(trans.position.y, 5),
                BaseMath.Round(trans.position.z, 5)
            };
            this.rotation = new List<float> {
                BaseMath.Round(trans.rotation.x, 5),
                BaseMath.Round(trans.rotation.y, 5),
                BaseMath.Round(trans.rotation.z, 5),
                BaseMath.Round(trans.rotation.w, 5)
            };
            this.scale = new List<float> {
                BaseMath.Round(trans.localScale.x, 5),
                BaseMath.Round(trans.localScale.y, 5),
                BaseMath.Round(trans.localScale.z, 5)
            };
            // position = new List<float> { Mathf.Round(trans.position.x * 10000) / 10000, Mathf.Round(trans.position.y * 10000) / 10000, Mathf.Round(trans.position.z * 10000) / 10000 };
            // rotation = new List<float> { Mathf.Round(trans.rotation.x * 10000) / 10000, Mathf.Round(trans.rotation.y * 10000) / 10000, Mathf.Round(trans.rotation.z * 10000) / 10000, Mathf.Round(trans.rotation.w * 10000) / 10000 };
            // scale = new List<float> { Mathf.Round(trans.localScale.x * 10000) / 10000, Mathf.Round(trans.localScale.y * 10000) / 10000, Mathf.Round(trans.localScale.z * 10000) / 10000 };
        }

        public ClientTransform()
        {
        }

        public Vector3 GetPosition()
        {
            return new Vector3(position[0], position[1], position[2]);
        }
        public Vector3 GetScale()
        {
            return new Vector3(scale[0], scale[1], scale[2]);
        }
        public Quaternion GetRotation()
        {
            return new Quaternion(rotation[0], rotation[1], rotation[2], rotation[3]);
        }
        public List<float> getRotation()
        {
            return this.rotation;
        }

        public void setRotation(List<float> rotation)
        {
            this.rotation = rotation;
        }

        public List<float> getScale()
        {
            return this.scale;
        }

        public void setScale(List<float> scale)
        {
            this.scale = scale;
        }
    }
}