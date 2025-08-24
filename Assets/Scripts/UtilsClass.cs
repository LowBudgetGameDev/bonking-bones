using UnityEngine;
using UnityEngine.Rendering;

public static class UtilsClass
{
    private static Camera mainCamera;

    public static Camera GetMainCamera()
    {
        if (mainCamera == null) mainCamera = Camera.main;

        return mainCamera;
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;

        Vector3 mouseWorldPosition = GetMainCamera().ScreenToWorldPoint(mousePosition);

        mouseWorldPosition.z = 0;

        return mouseWorldPosition;
    }

    public static float VectorToAngleDegrees(Vector3 vector)
    {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }

    public static Vector3 AngleDegreesToVector(float angleDegrees)
    {
        float angleRadians = angleDegrees * Mathf.Deg2Rad;

        return new Vector3(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));
    }

    public static string EncodeTexture2DToString(Texture2D texture2D)
    {
        // Encode texture into PNG
        byte[] textureBytes = texture2D.EncodeToPNG();

        // Convert to Base64 string
        string encoded = System.Convert.ToBase64String(textureBytes);

        return encoded;
    }

    public static Texture2D DecodeStringToTexture2D(string encoded)
    {
        // Decode Base64 back into bytes
        byte[] textureBytes = System.Convert.FromBase64String(encoded);

        // Create new Texture2D and load bytes
        Texture2D texture = new Texture2D(2, 2); // size doesn’t matter, will resize
        texture.LoadImage(textureBytes); // auto-resizes texture
        return texture;
    }
}
