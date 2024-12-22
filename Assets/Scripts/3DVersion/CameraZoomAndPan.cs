using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Camera))]
public class CameraZoomAndPan : MonoBehaviour
{
    private Camera cam => GetComponent<Camera>();           // La cam�ra principale

    [SerializeField] Transform target;            // L'objet cible que la cam�ra doit suivre (ex. l'image ou un objet 3D)

    [Header("Zoom")]
    [SerializeField] float zoomSpeed = 1f;       // Vitesse du zoom
    [SerializeField] float smoothZoomSpeed = 2f;  // Vitesse du mouvement en douceur lors du recentrage de la cam�ra
    [SerializeField] float minZoom = 5f;          // Zoom minimum (distance entre la cam�ra et la cible)
    public float MinZoom { get { return minZoom; } }
    [SerializeField] float maxZoom = 20f;         // Zoom maximum
    public float MaxZoom { get { return maxZoom; } }
    private float previousZoom;         // Pour suivre le zoom pr�c�dent

    [Header("Pan")]
    [SerializeField] float panSpeed = 0.1f;       // Vitesse du d�placement de la cam�ra
    [SerializeField] float limitsMultiplier = 2f;
    private Vector2 minPanLimit;         // Limite minimale de la cam�ra (X, Y)
    private Vector2 maxPanLimit;         // Limite maximale de la cam�ra (X, Y)

    // G�rer le zoom
    void HandleZoom(float zoomValue)
    {
        float newZoom;

        if(zoomValue < 3)
        {
            // Modifier le champ de vision (orthographicSize) en fonction du d�filement de la souris
            newZoom = cam.orthographicSize - zoomValue * zoomSpeed;

            // Limiter la taille du champ de vision entre minZoom et maxZoom
            newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);

            // Appliquer le nouveau zoom
            cam.orthographicSize = newZoom;

            GameEvent.current.onUpdateZoom?.Invoke(newZoom);
        }
        else
        {
            newZoom = zoomValue;

            cam.orthographicSize = newZoom;
        }

        Debug.Log("Zoom: " + newZoom);

        // Si nous d�zoomons (c'est-�-dire si le zoom actuel est plus grand que le zoom pr�c�dent), nous recentrons la cam�ra
        if (newZoom > previousZoom)
        {
            CenterCameraSmoothly();
        }

        // Mettre � jour le zoom pr�c�dent pour la prochaine comparaison
        previousZoom = newZoom;
    }

    // G�rer le d�placement de la cam�ra
    void HandlePan(Vector2 delta)
    {
        // Calculer les limites dynamiques en fonction du zoom actuel
        AdjustCameraBounds();

        // D�placer la cam�ra en fonction du delta de la souris
        Vector3 move = new Vector3(-delta.x, -delta.y, 0) * panSpeed;

        // Appliquer le mouvement de la cam�ra
        Vector3 newPosition = transform.position + move;

        // Limiter la position de la cam�ra en fonction des limites d�finies
        newPosition.x = Mathf.Clamp(newPosition.x, minPanLimit.x, maxPanLimit.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minPanLimit.y, maxPanLimit.y);

        // Appliquer la position limit�e � la cam�ra
        transform.position = newPosition;
    }

    // Recentrer doucement la cam�ra lorsque l'on d�zoome
    void CenterCameraSmoothly()
    {
        // Calculer un facteur de vitesse bas� sur la taille actuelle du zoom.
        // Plus la cam�ra est proche du plein �cran (zoom faible), plus le mouvement sera rapide.
        float zoomFactor = Mathf.InverseLerp(minZoom, maxZoom, cam.orthographicSize); // Normalisation de la taille du zoom

        // Vous pouvez ajuster cette fonction pour obtenir l'effet souhait�, ici nous utilisons un facteur exponentiel
        float smoothFactor = Mathf.Pow(zoomFactor, 2); // Augmente progressivement la vitesse en fonction du zoom

        // Calculer la position cible, ici nous utilisons le centre de l'objet ou un autre point (par exemple, (0,0))
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

        // Lerp pour d�placer la cam�ra vers la position centr�e avec une transition douce
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothZoomSpeed * smoothFactor * Time.deltaTime);
    }

    void AdjustCameraBounds()
    {
        if (target == null)
            return;

        // Calculer la taille de l'objet en fonction de son Renderer ou de ses dimensions
        SpriteRenderer spriteRenderer = target.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Calculer les dimensions du sprite en fonction de son bounds
            Vector2 spriteSize = spriteRenderer.bounds.size;

            // Ajuster les limites en fonction de la taille du sprite et du zoom
            float cameraHeight = cam.orthographicSize * 2;
            float cameraWidth = cameraHeight * cam.aspect;

            // Calculer les limites avec un centrage parfait (gauche, droite, haut, bas)
            // Nous allons ajuster les positions pour que la cam�ra couvre toute l'image de mani�re centr�e.

            float halfSpriteWidth = spriteSize.x / 2;
            float halfSpriteHeight = spriteSize.y / 2;

            // Adapter les limites pour que l'image enti�re soit visible � l'�cran
            minPanLimit.x = -halfSpriteWidth * limitsMultiplier + cameraWidth / 2;
            maxPanLimit.x = halfSpriteWidth * limitsMultiplier - cameraWidth / 2;
            minPanLimit.y = -halfSpriteHeight * limitsMultiplier + cameraHeight / 2;
            maxPanLimit.y = halfSpriteHeight * limitsMultiplier - cameraHeight / 2;
        }
    }

    private void OnEnable()
    {
        GameEvent.current.onMouseZoom += HandleZoom;
        GameEvent.current.onSliderZoom += HandleZoom;

        GameEvent.current.onPan += HandlePan;
    }

    private void OnDisable()
    {
        GameEvent.current.onMouseZoom -= HandleZoom;
        GameEvent.current.onSliderZoom -= HandleZoom;

        GameEvent.current.onPan -= HandlePan;
    }
}
