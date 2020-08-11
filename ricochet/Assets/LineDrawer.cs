using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LineDrawer : MonoBehaviour
{
	public Camera m_camera;
    public Material m_drawnMaterial;
    public float m_cDrawWidth = 0.1f;
    public GameObject m_ball;
    public Vector2 m_initalBallSpeed = new Vector2(0,2);

    private bool m_bDrawing = false;
    private LineRenderer m_drawingLineRenderer;
    private GameObject m_drawingGameObject;
    private Vector2 m_startDrawingPosition;

    void Update()
    {
        if(b_isToggleDraw && !b_gameInPlay)
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_startDrawingPosition = m_camera.ScreenToWorldPoint ( Input.mousePosition );
                m_bDrawing = true;

                m_drawingGameObject = new GameObject();
                m_drawingGameObject.transform.position = m_startDrawingPosition;
                m_drawingGameObject.AddComponent<LineRenderer>();
                m_drawingGameObject.tag = "Barrier";

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
                
                //Set edge collider points from line drawing points, offset by gameobject position
                List<Vector2> points = new List<Vector2>();
                points.Add(m_drawingLineRenderer.GetPosition(0) - m_drawingGameObject.transform.position);
                points.Add(m_drawingLineRenderer.GetPosition(1) - m_drawingGameObject.transform.position);
                EdgeCollider2D edgeCollider2D = m_drawingGameObject.AddComponent<EdgeCollider2D>();
                edgeCollider2D.points = points.ToArray();
            }
            else if (m_bDrawing)
            {
                Vector2 mousePosition = m_camera.ScreenToWorldPoint ( Input.mousePosition );
                m_drawingLineRenderer.SetPosition(1, mousePosition);
            }
        }
        else if(!b_gameInPlay)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(m_camera.ScreenToWorldPoint(Input.mousePosition),0.2f);
                foreach(Collider2D collider in colliders)
                {
                    if(collider.gameObject.tag == "Barrier")
                    {
                        Destroy(collider.gameObject);
                    }
                }
            }
        }
        else
        {
            //game is in play
        }
    }

    public Button btn_toggleGoReset;
    public Sprite sprite_go;
    public Sprite sprite_reset;
    private bool b_gameInPlay = false;
    public void toggleGoReset()
    {
        if(!b_gameInPlay)
        {
            //Start game
            m_ball.GetComponent<Rigidbody2D>().velocity = m_initalBallSpeed;

            btn_toggleGoReset.image.sprite = sprite_reset;
            b_gameInPlay = true;
        }
        else //Time to edit... game not in play
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public Button btn_toggleDrawErase;
    public Sprite sprite_draw;
    public Sprite sprite_erase;
    private bool b_isToggleDraw = true;
    public void toggleDrawErase()
    {
        if(b_isToggleDraw)
        {
            btn_toggleDrawErase.image.sprite = sprite_draw;
            b_isToggleDraw = false;
        }
        else
        {
            btn_toggleDrawErase.image.sprite = sprite_erase;
            b_isToggleDraw = true;
        }
        
    }
}
