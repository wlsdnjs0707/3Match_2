using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public sealed class Board : MonoBehaviour
{
    public static Board Instance { get; private set; }

    [SerializeField] private AudioClip collectSound;
    [SerializeField] private AudioSource _audioSource;

    public Row[] rows;
    public Tile[,] Tiles { get; private set; }

    public GameObject gameEndUI;

    public Button exitButton;

    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private TextMeshProUGUI coinText;

    private int reward = 0;

    private bool whilePopping = false;
    private bool whileSwapping = false;

    // Dimension 0
    public int Width => Tiles.GetLength(0);

    // Dimension 1
    public int Height => Tiles.GetLength(1);

    private readonly List<Tile> _selection = new List<Tile>();

    private const float TweenDuration = 0.25f;

    private void Awake() => Instance = this;

    // Start is called before the first frame update
    private void Start()
    {
        gameEndUI.SetActive(false);

        Tiles = new Tile[rows.Max(row => row.tiles.Length), rows.Length];

        for (var y=0; y < Height; y++)
        {
            for(var x=0; x < Width; x++)
            {
                var tile = rows[y].tiles[x];

                tile.x = x;
                tile.y = y;

                Tiles[x, y] = rows[y].tiles[x];

                tile.Item = ItemDatabase.Items[UnityEngine.Random.Range(0, ItemDatabase.Items.Length)];
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public async void Select(Tile tile)
    {
        if (whilePopping == false && whileSwapping == false)
        {
            if (!_selection.Contains(tile))
            {
                // Second Tile Select
                if (_selection.Count > 0)
                {
                    // Select Adjacent Tile
                    if (Array.IndexOf(_selection[0].Neighbors, tile) != -1)
                    {
                        _selection.Add(tile);
                    }
                    // Select Not Adjacent Tile
                    else
                    {
                        _selection.RemoveAt(0);
                    }
                }
                // First Tile Select
                else
                {
                    _selection.Add(tile);
                }
            }
        }

        if (_selection.Count < 2) return;

        //Debug.Log($"Selected tiles at ({_selection[0].x},{_selection[0].y}) and ({_selection[1].x}, {_selection[1].y})");

        await Swap(_selection[0], _selection[1]);

        if (CanPop())
        {
            Pop();
        }
/*        else
        {
            await Swap(_selection[0], _selection[1]);
        }*/

        MoveCountManager.Instance.Count -= 1;

        MoveCountCheck();

        _selection.Clear();
    }

    public async Task Swap(Tile tile1, Tile tile2)
    {
        if (whileSwapping == false)
        {
            whileSwapping = true;

            var icon1 = tile1.icon;
            var icon2 = tile2.icon;

            var icon1Transform = icon1.transform;
            var icon2Transform = icon2.transform;

            var sequence = DOTween.Sequence();

            sequence.Join(icon1Transform.DOMove(icon2Transform.position, TweenDuration)).Join(icon2Transform.DOMove(icon1Transform.position, TweenDuration));

            await sequence.Play().AsyncWaitForCompletion();

            icon1Transform.SetParent(tile2.transform);
            icon2Transform.SetParent(tile1.transform);

            tile1.icon = icon2;
            tile2.icon = icon1;

            var tile1Item = tile1.Item;

            tile1.Item = tile2.Item;
            tile2.Item = tile1Item;

            whileSwapping = false;
        }

    }

    private bool CanPop()
    {
        for (var y=0; y < Height; y++)
        {
            for (var x=0; x < Width; x++)
            {
                if(Tiles[x,y].GetConnectedTiles().Skip(1).Count() >= 2)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private async void Pop()
    {
        if (whilePopping == false)
        {
            whilePopping = true;

            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var tile = Tiles[x, y];

                    var connectedTiles = tile.GetConnectedTiles();

                    if (connectedTiles.Skip(1).Count() < 2) continue;

                    // Damage Dealt
                    HealthManager.Instance.Health -= connectedTiles.Count;

                    // Play Sound Effect
                    _audioSource.PlayOneShot(collectSound);

                    // Deflate
                    var deflateSequence = DOTween.Sequence();
                    foreach (var connectedTile in connectedTiles)
                    {
                        deflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.zero, TweenDuration));
                    }
                    await deflateSequence.Play().AsyncWaitForCompletion();

                    // Inflate
                    var inflateSequence = DOTween.Sequence();
                    foreach (var connectedTile in connectedTiles)
                    {
                        connectedTile.Item = ItemDatabase.Items[UnityEngine.Random.Range(0, ItemDatabase.Items.Length)];
                        inflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.one, TweenDuration));
                    }
                    await inflateSequence.Play().AsyncWaitForCompletion();

                    // Re-Check
                    x = 0;
                    y = 0;
                }
            }

            whilePopping = false;
        }

        HealthCheck();
    }

    private void HealthCheck()
    {

        if (HealthManager.Instance.Health <= 0)
        {
            // Win Event
            gameEndUI.SetActive(true);

            if (SceneManager.GetActiveScene().name == "Stage1")
            {
                GameManager.instance.stage2access = true;
                reward = 100;
            }

            if (SceneManager.GetActiveScene().name == "Stage2")
            {
                GameManager.instance.stage3access = true;
                reward = 500;
            }

            if (SceneManager.GetActiveScene().name == "Stage3")
            {
                // Ending
            }

            statusText.SetText($"YOU WIN");
            coinText.SetText($"{reward}");
            GameManager.instance.coin += reward;

            // Stop
            Time.timeScale = 0.0f;
        }
    }

    private void MoveCountCheck()
    {
        // Prevent Error
        if (whilePopping == true || whileSwapping == true)
        {
            Invoke("MoveCountCheck", 1);
            return;
        }

        if (MoveCountManager.Instance.Count <= 0)
        {
            // Disable Select
            whilePopping = true;
            whileSwapping = true;

            // Lose Event
            gameEndUI.SetActive(true);

            if (SceneManager.GetActiveScene().name == "Stage1")
            {
                reward = 50 - HealthManager.Instance.Health;
            }

            if (SceneManager.GetActiveScene().name == "Stage2")
            {
                reward = 150 - HealthManager.Instance.Health;
            }

            if (SceneManager.GetActiveScene().name == "Stage3")
            {
                reward = 300 - HealthManager.Instance.Health;
            }

            statusText.SetText($"YOU LOSE");
            coinText.SetText($"{reward}");
            GameManager.instance.coin += reward;

            // Stop
            Time.timeScale = 0.0f;
        }
    }

    public void ExitGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main");
    }
}
