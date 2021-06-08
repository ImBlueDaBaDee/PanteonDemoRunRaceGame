using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexPaint : MonoBehaviour
{
    [SerializeField] private float brushSize;
    private GameObject wallToPaint;
    private Mesh mesh;
    private MeshFilter meshFilter;
    private Color paintingColor;
    private RaycastHit curHit;
    private List<Color> paintedVerts;
    private Color[] colors;
    private Vector3[] verts;


    private float sqrMag;
    private float paintedCount, meshColorsLength;

    public int matchRate;

    private PlayerMovement playerScript;

    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        brushSize = 1;
        paintingColor = Color.red;
        meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.sharedMesh;
        wallToPaint = this.gameObject;
        verts = mesh.vertices;
        if (mesh.colors.Length > 0)
        {
            colors = mesh.colors;
            for (int i = 0; i < mesh.colors.Length; i++)
            {
                colors[i] = Color.white;
            }
        }
        else
        {
            colors = new Color[verts.Length];
            for (int i = 0; i < verts.Length; i++)
            {
                colors[i] = Color.white;
            }
        }

        mesh.colors = colors;

        matchRate = 0;

    }


    void Update()
    {
        paintedVerts = new List<Color>();
        for (int i = 0; i < mesh.colors.Length; i++)
        {
            if (mesh.colors[i] != Color.white)
            {
                paintedVerts.Add(mesh.colors[i]);
            }
        }
        paintedCount = paintedVerts.Count;
        meshColorsLength = mesh.colors.Length;
        matchRate = Mathf.RoundToInt((paintedCount / meshColorsLength) * 100);
    }
    private void FixedUpdate()
    {
        int layermask = 1 << 10;
        Ray worldRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(worldRay, out curHit, float.MaxValue, layermask))
        {
            if (Input.GetMouseButton(0))
            {
                PaintVertexColor();
            }
        }
    }

    void PaintVertexColor()
    {
        if (mesh && playerScript.isFinished)
        {
            for (int i = 0; i < verts.Length; i++)
            {
                Vector3 vertPos = wallToPaint.transform.TransformPoint(verts[i]);
                sqrMag = (vertPos - curHit.point).sqrMagnitude;
                if (sqrMag <= brushSize/100) // Defines the vertices to paint according to their distance to pointer.
                                             // brushSize/100 operation is just for using better numbers since everything in the scene is so small.(SORRY)                 
                {
                    colors[i] = paintingColor;
                }
            }
            mesh.colors = colors;
        }
    }
}
