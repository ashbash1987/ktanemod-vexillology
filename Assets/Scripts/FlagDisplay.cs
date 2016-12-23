using UnityEngine;

[ExecuteInEditMode()]
public sealed class FlagDisplay : MonoBehaviour
{
    public MeshRenderer MeshRenderer = null;

    public int TextureWidth = 1024;
    public float FlagElementRelativeWidth = 0.1f;

    private FlagDescriptor _descriptor = null;

    private void Start()
    {
        GenerateFlagDescriptor();
        RefreshFlag();        
    }

    private void GenerateFlagDescriptor()
    {
        FlagBuilder builder = new FlagBuilder(FlagNation.Picri);

        _descriptor = new FlagDescriptor(builder);

        Debug.Log(_descriptor.ToString());
    }    

    private void RefreshFlag()
    {
        Vector3 scale = MeshRenderer.transform.localScale;

        int textureHeight = Mathf.RoundToInt((TextureWidth * scale.y) / scale.x);

        Texture2D texture = new Texture2D(TextureWidth, textureHeight, TextureFormat.ARGB32, true, true);
        texture.wrapMode = TextureWrapMode.Clamp;
        _descriptor.Draw(texture);

        Material material = new Material(MeshRenderer.sharedMaterial);
        material.mainTexture = texture;

        MeshRenderer.material = material;
    }
}
