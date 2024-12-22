using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Camera))]
public class CameraZoomAndPan : MonoBehaviour
{
    private Camera cam => GetComponent<Camera>();           // La caméra principale

    [SerializeField] Transform target;            // L'objet cible que la caméra doit suivre (ex. l'image ou un objet 3D)

    [Header("Zoom")]
    [SerializeField] float zoomSpeed = 1f;       // Vitesse du zoom
    [SerializeField] float smoothZoomSpeed = 2f;  // Vitesse du mouvement en douceur lors du recentrage de la caméra
    [SerializeField] float minZoom = 5f;          // Zoom minimum (distance entre la caméra et la cible)
    public float MinZoom { get { return minZoom; } }
    [SerializeField] float maxZoom = 20f;         // Zoom maximum
    public float MaxZoom { get { return maxZoom; } }
    private float previousZoom;         // Pour suivre le zoom précédent

    [Header("Pan")]
    [SerializeField] float panSpeed = 0.1f;       // Vitesse du déplacement de la caméra
    [SerializeField] float limitsMultiplier = 2f;
    private Vector2 minPanLimit;         // Limite minimale de la caméra (X, Y)
    private Vector2 maxPanLimit;         // Limite maximale de la caméra (X, Y)

    // Gérer le zoom
    void HandleZoom(float zoomValue)
    {
        float newZoom;

        if(zoomValue < 3)
        {
            // Modifier le champ de vision (orthographicSize) en fonction du défilement de la souris
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

        // Si nous dézoomons (c'est-à-dire si le zoom actuel est plus grand que le zoom précédent), nous recentrons la caméra
        if (newZoom > previousZoom)
        {
            CenterCameraSmoothly();
        }

        // Mettre à jour le zoom précédent pour la prochaine comparaison
        previousZoom = newZoom;
    }

    // Gérer le déplacement de la caméra
    void HandlePan(Vector2 delta)
    {
        // Calculer les limites dynamiques en fonction du zoom actuel
        AdjustCameraBounds();

        // Déplacer la caméra en fonction du delta de la souris
        Vector3 move = new Vector3(-delta.x, -delta.y, 0) * panSpeed;

        // Appliquer le mouvement de la caméra
        Vector3 newPosition = transform.position + move;

        // Limiter la position de la caméra en fonction des limites définies
        newPosition.x = Mathf.Clamp(newPosition.x, minPanLimit.x, maxPanLimit.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minPanLimit.y, maxPanLimit.y);

        // Appliquer la position limitée à la caméra
        transform.position = newPosition;
    }

    // Recentrer doucement la caméra lorsque l'on dézoome
    void CenterCameraSmoothly()
    {
        // Calculer un facteur de vitesse basé sur la taille actuelle du zoom.
        // Plus la caméra est proche du plein écran (zoom faible), plus le mouvement sera rapide.
        float zoomFactor = Mathf.InverseLerp(minZoom, maxZoom, cam.orthographicSize); // Normalisation de la taille du zoom

        // Vous pouvez ajuster cette fonction pour obtenir l'effet souhaité, ici nous utilisons un facteur exponentiel
        float smoothFactor = Mathf.Pow(zoomFactor, 2); // Augmente progressivement la vitesse en fonction du zoom

        // Calculer la position cible, ici nous utilisons le centre de l'objet ou un autre point (par exemple, (0,0))
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

        // Lerp pour déplacer la caméra vers la position centrée avec une transition douce
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
            // Nous allons ajuster les positions pour que la caméra couvre toute l'image de manière centrée.

            float halfSpriteWidth = spriteSize.x / 2;
            float halfSpriteHeight = spriteSize.y / 2;

            // Adapter les limites pour que l'image entière soit visible à l'écran
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
