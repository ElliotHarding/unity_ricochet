using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
	public Camera m_camera;
    public Material m_drawnMaterial;
    public float m_cDrawWidth = 0.1f;

    private bool m_bDrawing = false;
    private LineRenderer m_drawingLineRenderer;
    private GameObject m_drawingGameObject;
    private Vector2 m_startDrawingPosition;

    void Start()
    {
        
    }

    void Update()
    {
        if ( Input.GetMouseButtonDown(0))
		{
			m_startDrawingPosition = m_camera.ScreenToWorldPoint ( Input.mousePosition );
			m_bDrawing = true;

            m_drawingGameObject = new GameObject();
            m_drawingGameObject.transform.position = m_startDrawingPosition;
            m_drawingGameObject.AddComponent<LineRenderer>();

            m_drawingLineRenderer = m_drawingGameObject.GetComponent<LineRenderer>();
            m_drawingLineRenderer.positionCount = 2;
            m_drawingLineRenderer.material = m_drawnMaterial;
            m_drawingLineRenderer.startColor = Color.white;
            m_drawingLineRenderer.endColor = Color.white;
            m_drawingLineRenderer.startWidth = m_cDrawWidth;
            m_drawingLineRenderer.endWidth = m_cDrawWidth;
            m_drawingLineRenderer.useWorldSpace = true;
            m_drawingLineRenderer.SetPosition(0, m_startDrawingPosition);
            m_drawingLineRenderer.SetPosition(1, m_startDrawingPosition);
		}        
        else if (Input.GetMouseButtonUp(0))
        {
            m_bDrawing = false;

            List<Vector2> points = new List<Vector2>();
            points.Add(m_drawingLineRenderer.GetPosition(0));
            points.Add(m_drawingLineRenderer.GetPosition(1));

            EdgeCollider2D edgeCollider2D = m_drawingGameObject.AddComponent<EdgeCollider2D>();
            edgeCollider2D.points = points.ToArray();
        }
        else if (m_bDrawing)
        {
            Vector2 mousePosition = m_camera.ScreenToWorldPoint ( Input.mousePosition );
            m_drawingLineRenderer.SetPosition(1, mousePosition);
        }
    }
}
