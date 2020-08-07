using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
	protected LineRenderer m_LineRenderer;
	protected EdgeCollider2D m_EdgeCollider2D;
	protected Camera m_Camera;
	protected List<Vector2> m_Points;
    public Material m_drawnMaterial;

	public virtual LineRenderer lineRenderer
	{
		get
		{
			return m_LineRenderer;
		}
	}

	public virtual EdgeCollider2D edgeCollider2D
	{
		get
		{
			return m_EdgeCollider2D;
		}
	}

	public virtual List<Vector2> points
	{
		get
		{
			return m_Points;
		}
	}

	protected virtual void Awake ()
	{
		if ( m_LineRenderer == null )
		{
			Debug.LogWarning ( "DrawLine: Line Renderer not assigned, Adding and Using default Line Renderer." );
			CreateDefaultLineRenderer ();
		}
		if ( m_EdgeCollider2D == null)
		{
			Debug.LogWarning ( "DrawLine: Edge Collider 2D not assigned, Adding and Using default Edge Collider 2D." );
			CreateDefaultEdgeCollider2D ();
		}
		if ( m_Camera == null ) {
			m_Camera = Camera.main;
		}
		m_Points = new List<Vector2> ();
	}

	protected virtual void Update ()
	{
		if ( Input.GetMouseButtonDown ( 1 ) )
		{
			Reset ();
		}
		if ( Input.GetMouseButton ( 0 ) )
		{
			Vector2 mousePosition = m_Camera.ScreenToWorldPoint ( Input.mousePosition );
			if ( !m_Points.Contains ( mousePosition ) )
			{
				m_Points.Add ( mousePosition );
				m_LineRenderer.positionCount = m_Points.Count;
				m_LineRenderer.SetPosition ( m_LineRenderer.positionCount - 1, mousePosition );
				if ( m_EdgeCollider2D != null && m_Points.Count > 1 )
				{
					m_EdgeCollider2D.points = m_Points.ToArray ();
				}
			}
		}
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Up");
        }
	}

	protected virtual void Reset ()
	{
		if ( m_LineRenderer != null )
		{
			m_LineRenderer.positionCount = 0;
		}
		if ( m_Points != null )
		{
			m_Points.Clear ();
		}
		if ( m_EdgeCollider2D != null)
		{
			m_EdgeCollider2D.Reset ();
		}
	}

	protected virtual void CreateDefaultLineRenderer ()
	{
		m_LineRenderer = gameObject.AddComponent<LineRenderer> ();
		m_LineRenderer.positionCount = 0;
		m_LineRenderer.material = m_drawnMaterial;
		m_LineRenderer.startColor = Color.white;
		m_LineRenderer.endColor = Color.white;
		m_LineRenderer.startWidth = 0.2f;
		m_LineRenderer.endWidth = 0.2f;
		m_LineRenderer.useWorldSpace = true;
	}

	protected virtual void CreateDefaultEdgeCollider2D ()
	{
		m_EdgeCollider2D = gameObject.AddComponent<EdgeCollider2D> ();
	}

}
