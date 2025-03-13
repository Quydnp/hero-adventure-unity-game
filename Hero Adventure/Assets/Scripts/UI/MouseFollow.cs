using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private void Update()
    {
        if (ActiveWeapon.Instance.CurrentActiveWeapon is Sword) MouseFollowWithOffset();
        if (ActiveWeapon.Instance.CurrentActiveWeapon is Bow) FaceMouse();
    }

    private void FaceMouse()
    {
        if (Time.timeScale == 0f) return;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = transform.position - mousePosition;

        transform.right = -direction;
    }

    private void MouseFollowWithOffset()
    {
        var weaponCollider = PlayerController.Instance.GetWeaponCollider();
        if (Time.timeScale == 0f) return;
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(
            PlayerController.Instance.transform.position
        );

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
