using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    // Referensi ke objek induk yang berisi anak-anak untuk diubah warnanya
    public GameObject parentObject;

    // Variabel untuk menyimpan warna asli semua anak
    private Color[] originalColors;

    void Start()
    {
        if (parentObject != null)
        {
            // Dapatkan semua penyaji anak
            Renderer[] childRenderers = parentObject.GetComponentsInChildren<Renderer>();

            // Simpan warna aslinya
            originalColors = new Color[childRenderers.Length];
            for (int i = 0; i < childRenderers.Length; i++)
            {
                originalColors[i] = childRenderers[i].material.color;
            }
        }
    }

    // Metode mengubah warna semua anak menjadi merah
    public void ChangeAllChildrenColorToRed()
    {
        if (parentObject != null)
        {
            // Dapatkan semua penyaji anak
            Renderer[] childRenderers = parentObject.GetComponentsInChildren<Renderer>();

            // Ubah warnanya menjadi merah
            foreach (Renderer renderer in childRenderers)
            {
                renderer.material.color = Color.red;
            }
        }
    }

    // Cara mengembalikan warna semua anak ke warna aslinya
    public void ResetAllChildrenColor()
    {
        if (parentObject != null && originalColors != null)
        {
            // Dapatkan semua penyaji anak
            Renderer[] childRenderers = parentObject.GetComponentsInChildren<Renderer>();

            // Atur ulang warna ke warna aslinya
            for (int i = 0; i < childRenderers.Length; i++)
            {
                childRenderers[i].material.color = originalColors[i];
            }
        }
    }
}
