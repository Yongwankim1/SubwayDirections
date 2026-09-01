using System;

public class SubwayDirections
{
    public static Dictionary<string, Station> Stations = new Dictionary<string, Station>();

    static void Main(string[] args)
    {
        Init();
        Console.WriteLine("출발 역");
        string StartName = Console.ReadLine();
        FindStationInfo(StartName);
    }

    static void FindStationInfo(string stationName)
    {
        if(string.IsNullOrEmpty(stationName)) return;
        string findStation = stationName.Replace(" ","");
        Station station;
        
        if (Stations.ContainsKey(findStation))
        {
            station = Stations[findStation];
            Console.WriteLine($"{stationName}의 정보");
            if (station.To.Equals("_"))
            {
                Console.WriteLine($"이전역 {station.From}까지 걸리는 시간 {station.SegmentTimes[0] / 60}분 {station.SegmentTimes[0] % 60}초");
            }
            else if (station.From.Equals("_"))
            {
                Console.WriteLine($"다음역 {station.To}까지 걸리는 시간 {station.SegmentTimes[0] / 60}분 {station.SegmentTimes[0] % 60}초");
            }
            else
            {
                Console.WriteLine($"다음역 {station.To}까지 걸리는 시간 {station.SegmentTimes[0] / 60}분 {station.SegmentTimes[0] % 60}초");
                Console.WriteLine($"이전역 {station.From}까지 걸리는 시간 {station.SegmentTimes[0] / 60}분 {station.SegmentTimes[0] % 60}초");
            }
        }
    }

    private static void Init()
    {
        string[] railWayStation = { "용산", "남영", "서울역", "시청", "종각", "종로3가", "종로5가", "동대문", "동묘앞", "신설동", "제기동", "청량리" };
        int[] railWaySegmentTime = { 110, 120, 120, 100, 90, 90, 90, 80, 80, 90, 100 };
        AddStation(railWayStation, railWaySegmentTime, 1);
    }


    private static void AddStation(string[] stations, int[] segmentTime, int line)
    {
        if (stations.Length != segmentTime.Length + 1)
        {
            throw new ArgumentException("역의 개수는 구간 시간의 개수보다 1개 많아야 합니다.");
        }
        for (int i = 0; i < stations.Length; i++)
        {
            int[] segmentTimes;
            int[] lines = [line];
            Station station;
            if (i == 0)
            {
                segmentTimes = [segmentTime[i]];
                station = new Station(stations[i + 1].Replace(" ",""), "_", lines,segmentTimes);
            }
            else if (i == stations.Length - 1)
            {
                segmentTimes = [segmentTime[i - 1]];
                station = new Station("_",stations[i - 1].Replace(" ", ""), lines, segmentTimes);
            }
            else
            {
                segmentTimes = [segmentTime[i], segmentTime[i - 1]];
                station = new Station(stations[i + 1].Replace(" ", ""), stations[i-1].Replace(" ", ""), lines, segmentTimes);
            }

            Stations.Add(stations[i].Replace(" ", ""), station);
        }

    }
}