using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class Tile
{
    public string url;
    public int width;
    public int height;
}

[Serializable]
public class TileUrls
{
    public Tile[] tiles;
}


public class TileDownloader : MonoBehaviour
{
    public int checkerSize = 10;
    public GameObject tileMap;
    private string apiEndpoint = "https://quicklook.orientbell.com/Task/gettiles.php";
    private string savePath;
    private TileUrls tileUrlObj;
    IEnumerator Start()
    {
        savePath = Application.persistentDataPath;
        Debug.Log(savePath);
        // Get list of tiles from API
        UnityWebRequest www = UnityWebRequest.Get(apiEndpoint);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Parse response to extract tile URLs
            string[] tileUrls = ParseResponse(www.downloadHandler.text);

            // Download tiles and save to local storage
            foreach (string url in tileUrls)
            {
                //Debug.Log(url);
                string fileName = Path.GetFileName(url);
                string filePath = Path.Combine(savePath, fileName);

                using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
                {
                    yield return uwr.SendWebRequest();

                    if (uwr.result != UnityWebRequest.Result.Success)
                    {
                        Debug.Log(uwr.error);
                    }
                    else
                    {
                        var texture = DownloadHandlerTexture.GetContent(uwr);
                        byte[] bytes = texture.EncodeToPNG();
                        File.WriteAllBytes(filePath, bytes);
                    }
                }
            }

            // Create checker pattern using downloaded tiles
            CreateCheckerPattern(tileUrls);//, tileUrlObj.tiles[0].width, tileUrlObj.tiles[0].height);
        }
    }

    private string[] ParseResponse(string responseText)
    {
        //Debug.Log("API Response: " + responseText);

        // Deserialize response into TileUrls object
        tileUrlObj = JsonUtility.FromJson<TileUrls>("{\"tiles\":" + responseText + "}");

        // Extract tile URLs from Tile objects
        string[] urls = new string[tileUrlObj.tiles.Length];
        for (int i = 0; i < tileUrlObj.tiles.Length; i++)
        {
            urls[i] = tileUrlObj.tiles[i].url;
        }

        //Debug.Log("Parsed URLs: " + string.Join(", ", urls));

        return urls;
    }

    private void CreateCheckerPattern(string[] tileUrls)//, int width=2, int height=2)
    {
        string fileName = Path.GetFileName(tileUrls[0]);
        string filePath = Path.Combine(savePath, fileName);

        byte[] pngBytes1 = File.ReadAllBytes(filePath);
        Texture2D tex1 = new Texture2D(2, 2);
        tex1.LoadImage(pngBytes1);

        fileName = Path.GetFileName(tileUrls[1]);
        filePath = Path.Combine(savePath, fileName);

        byte[] pngBytes2 = File.ReadAllBytes(filePath);
        Texture2D tex2 = new Texture2D(2, 2);
        tex2.LoadImage(pngBytes2);

        // Create a new texture with a checker pattern
        int width = checkerSize * tex1.width;
        int height = checkerSize * tex1.height;
        Texture2D checkerTex = new Texture2D(width, height);
        for (int y = 0; y < height; y += tex1.height)
        {
            for (int x = 0; x < width; x += tex1.width)
            {
                bool useTex1 = ((x / tex1.width) % 2 == 0) == ((y / tex1.height) % 2 == 0);
                checkerTex.SetPixels(x, y, tex1.width, tex1.height, useTex1 ? tex1.GetPixels() : tex2.GetPixels());
            }
        }
        checkerTex.Apply();
        Renderer renderer = tileMap.GetComponent<Renderer>();
        renderer.material.SetTexture("_MainTex", checkerTex);
        //renderer.material.mainTextureScale = new Vector2(4, 4); // Repeat the texture 4 times in each direction
    }

}
