public struct StationData
{
    public string To;
    public int SegmentTime;

    public StationData(string to, int segmentTime)
    {
        To = to;
        SegmentTime = segmentTime;
    }
}

public class Station
{
    public int[] Lines;
    public List<StationData> StationDatas = new();

    public Station(int line)
    {
        Lines = [line];
    }

    public void AddStationData(string to, int segmentTime)
    {
        StationDatas.Add(new StationData(to, segmentTime));
    }

    public void AddLine(int line)
    {
        if (Lines.Length == 0 || Lines == null)
        {
            Lines = [line];
            return;
        }

        if (Lines.Contains(line)) return;

        int[] lines = new int[Lines.Length + 1];

        for (int i = 0; i < Lines.Length; i++)
        {
            lines[i] = Lines[i];
        }

        lines[^1] = line;
        Lines = lines;
    }
}