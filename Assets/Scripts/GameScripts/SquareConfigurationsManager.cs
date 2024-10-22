using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class SquareConfigurationsManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private GameManager gameManager;
    private Dictionary<string, SquareConfiguration> _configurations;

    private static int _reverseIndex;
    private void Start()
    {
        _reverseIndex = 0;
        _configurations = new Dictionary<string, SquareConfiguration>();

        var filePaths = Directory.GetFiles(Application.dataPath + "/StreamingAssets/", "*.txt", SearchOption.TopDirectoryOnly);
        
        foreach (var filePath in filePaths)
        {
            var filename = Path.GetFileNameWithoutExtension(filePath);
            _configurations.Add(filename, new SquareConfiguration(File.ReadAllLines(filePath)));
        }
        
        foreach (var filePath in filePaths)
        {
            var filename = Path.GetFileNameWithoutExtension(filePath);
            dropdown.options.Add(new TMP_Dropdown.OptionData(){text=filename});
        }
        dropdown.RefreshShownValue();
    }

    public SquareConfiguration GetTemplate(Vector3 anchor)
    {
        var x = Utils.GetCellCoordinates(anchor).x;
        var y = Utils.GetCellCoordinates(anchor).y;
        
        var filename = dropdown.options[dropdown.value].text;
        
        return new SquareConfiguration(_configurations[filename].GetSquares(), new Vector2Int(x, y), _reverseIndex);
    }

    public static void ReverseConfiguration()
    {
        _reverseIndex += 1;
        _reverseIndex %= 4;
    }
}
