using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gamekit3D
{
    [RequireComponent(typeof(Collider))]
    public class InteractOnTrigger : MonoBehaviour
    {
        public LayerMask layers;
        public UnityEvent OnEnter, OnExit;
        new Collider collider;
        public InventoryController.InventoryChecker[] inventoryChecks;
        public GameObject WinningCanvas;
        public GameObject WinningPanel;
        void Reset()
        {
            layers = LayerMask.NameToLayer("Everything");
            collider = GetComponent<Collider>();
            collider.isTrigger = true;
        }

        void OnTriggerEnter(Collider other)
        {
            if (0 != (layers.value & 1 << other.gameObject.layer))
            {
                ExecuteOnEnter(other);
            }

        }

        protected virtual void ExecuteOnEnter(Collider other)
        {            
            if (WinningCanvas != null)
            {
                WinningCanvas.SetActive(true);
            }
            if (WinningPanel != null)
            {
                WinningPanel.GetComponent<CanvasRenderer>().SetAlpha(0.1f);
                WinningPanel.GetComponent<UnityEngine.UI.Image>().CrossFadeAlpha(5f, 1f, false);

            }

            OnEnter.Invoke();
            for (var i = 0; i < inventoryChecks.Length; i++)
            {
                inventoryChecks[i].CheckInventory(other.GetComponentInChildren<InventoryController>());
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (0 != (layers.value & 1 << other.gameObject.layer))
            {
                ExecuteOnExit(other);
            }
        }

        protected virtual void ExecuteOnExit(Collider other)
        {
            OnExit.Invoke();
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawIcon(transform.position, "InteractionTrigger", false);
        }

        void OnDrawGizmosSelected()
        {
            //need to inspect events and draw arrows to relevant gameObjects.
        }

    } 
}


