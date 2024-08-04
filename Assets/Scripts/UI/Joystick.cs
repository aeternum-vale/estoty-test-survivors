using UnityEngine;
using UnityEngine.EventSystems;
using Gameplay;

namespace UI
{
	public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IInputSource
	{
		[SerializeField] private float _radius;
		[SerializeField] private RectTransform _handle;

		private bool _canDrag;

		public Vector2 MovementDelta => (_handle.position - transform.position) / _radius;

		public void OnPointerDown(PointerEventData eventData)
		{
			if (Vector2.Distance(eventData.position, transform.position) <= _radius)
			{
				_canDrag = true;
				OnDrag(eventData);
			}
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (!_canDrag) return;

			Vector2 joystickCenter = transform.position;

			if (Vector2.Distance(eventData.position, transform.position) <= _radius)
				_handle.position = eventData.position;
			else
				_handle.position = joystickCenter + (eventData.position - joystickCenter).normalized * _radius;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			_handle.position = transform.position;
			_canDrag = false;
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(transform.position, _radius);
		}
	}
}