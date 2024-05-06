using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using UnityEngine;

public class SlidingHangingPic : MonoBehaviour
{
    [SerializeField] private Transform gameTransform;
    [SerializeField] private Transform piecePrefab;

    private List<Transform> pieces;
    private int emptyLocation;
    private int size;
    public bool shuffling = false;
    public bool canWin = false;
    public GameObject incenseStick;


    private void Start()
    {
        pieces = new List<Transform>();
        size = 3;
        CreateGamePieces(0.01f);
        Shuffle();
        //StartCoroutine(WaitShuffle(0.01f));
    }

    private void Update()
    {
        // Check for completion
        if(!shuffling )
        {
            if (canWin)//CheckCompletion())
            {
                StartCoroutine(DropIncenseStick());
                Debug.Log("can win now");
            }
            else
            {
                Debug.Log("cannot win now");
            }
            
        }








        // On click send out ray to see if we click a piece
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                // Go through the list, the index tells us the position
                for(int i = 0; i < pieces.Count; i++)
                {
                    if (pieces[i] == hit.transform)
                    {
                        // Check each direrction to see if valid move
                        // We break out on success so we don't carry on and swap back again
                        if (SwapIfValid(i, -size, size)) { break; }
                        if (SwapIfValid(i, +size, size)) { break; }
                        if (SwapIfValid(i, -1, 0)) { break; }
                        if (SwapIfValid(i, +1, size - 1)) {  break; }

                    }
                }
            }
        }

        
    }

    // colCheck is used to stop horizontal moves wrapping
    private bool SwapIfValid(int i, int offset, int colCheck)
    {
        if(((i % size) != colCheck) && ((i + offset) == emptyLocation))
        {
            // swap them in game state
            (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);
            // Swap their transforms
            (pieces[i].localPosition, pieces[i + offset].localPosition) = ((pieces[i + offset].localPosition, pieces[i].localPosition));
            // Update empty location
            emptyLocation = i;
            return true;
        }
        return false;
    }
    private void CreateGamePieces(float gapThickness)
    {
        float width = 1 / (float)size;
        for(int row = 0; row < size; row++)
        {
            for(int col = 0; col < size; col++)
            {
                Transform piece = Instantiate(piecePrefab, gameTransform);

                pieces.Add(piece);

                piece.localPosition = new Vector3(-1 + (2 * width * col) + width,
                    +1 - (2 * width * row) + width, 0);
                piece.localScale = ((2 * width) - gapThickness) * Vector3.one;
                piece.name = $"{(row * size) + col}";

                if((row == size - 1) &&(col == size - 1))
                {
                    emptyLocation = (size * size) - 1;
                    piece.gameObject.SetActive(false);
                }
                else
                {
                    float gap = gapThickness / 2;
                    Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                    Vector2[] uv = new Vector2[4];

                    uv[0] = new Vector2((width * col) + gap,1 - ((width *(row +1)) - gap));
                    uv[1] = new Vector2((width * (col +1)) - gap, 1 - ((width * (row + 1)) - gap));
                    uv[2] = new Vector2((width * col) + gap, 1 - ((width * row) + gap));
                    uv[3] = new Vector2((width * (col + 1)) - gap, 1 - ((width * row) + gap));

                    mesh.uv = uv;
                }
            }
        }
    }
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        Debug.Log("hey I am starting sliding puzzle");
    }

    private bool CheckCompletion()
    {
        for( int i = 0; i< pieces.Count; i++ )
        {
            if (pieces[i].name != $"{i}")
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator WaitShuffle(float duration)
    {
        yield return new WaitForSeconds(duration);
        Shuffle();
        shuffling = false;
    }

    private void Shuffle()
    {
        int count = 0;
        int last = 0;
        while (count < (size * size * size))
        {
            // Pick a random location
            int rnd = Random.Range(0, size * size);

            // Only thing we forbid is undoing the last move
            if (rnd == last)
            {
                continue;
            }
            last = emptyLocation;
            // Try surrounding spaces looking for valid move
            if ( SwapIfValid(rnd, -size, size))
            {
                count++;
            }
            else if (SwapIfValid(rnd, +size, size))
            {
                count++;
            }
            else if (SwapIfValid(rnd, -1, 0))
            {
                count++;
            }
            else if( SwapIfValid(rnd, +1, size -1))
            {
                count++;
            }
        }
    }
    public IEnumerator DropIncenseStick()
    {
        yield return new WaitForSeconds(0.5f);
        incenseStick.SetActive(true);
        Destroy(gameObject);
    }
}
