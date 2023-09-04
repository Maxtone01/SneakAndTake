using Assets.Scripts.ClassSystem;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Observers
{
    public class SneakerObserverActions : MonoBehaviour
    {
        [SerializeField] private BoxObserver _boxObserver;

        [SerializeField] private PlacableZoneObserver _placableObserver;

        [SerializeField] private PlayerActions _playerActions;

        private PlayerSneaker _playerController;

        private void OnDestroy()
        {
            _boxObserver.onTriggerEnter -= OnTriggerBox;
            _boxObserver.onTriggerExit -= OnTriggerExitBox;
            _placableObserver.onTriggerEnter -= OnPlacableEnter;
            _placableObserver.onTriggerExit -= OnPlacableExit;
        }

        private void Awake()
        {
            _playerController = GetComponent<PlayerSneaker>();
            _playerActions = GetComponentInParent<PlayerActions>();
        }

        private void Start()
        {
            _boxObserver.onTriggerEnter += OnTriggerBox;
            _boxObserver.onTriggerExit += OnTriggerExitBox;
            _placableObserver.onTriggerEnter += OnPlacableEnter;
            _placableObserver.onTriggerExit += OnPlacableExit;
        }

        private void OnTriggerBox(BoxObserver obj)
        {
            _playerController.SetGrabState(true);
            _playerActions._grabbableItem = obj.gameObject;
        }
        private void OnTriggerExitBox(BoxObserver obj)
        {
            _playerController.SetGrabState(false);
            _playerActions._grabbableItem = null;
        }

        private void OnPlacableEnter(PlacableZoneObserver obj)
        {
            _playerController.SetPlaceState(true);
        }

        private void OnPlacableExit(PlacableZoneObserver obj)
        {
            _playerController.SetPlaceState(false);
        }

    }
}
