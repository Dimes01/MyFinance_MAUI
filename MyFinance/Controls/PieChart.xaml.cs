using Microsoft.Maui.Controls.Shapes;
using Color = Microsoft.Maui.Graphics.Color;
using Path = Microsoft.Maui.Controls.Shapes.Path;
using Point = Microsoft.Maui.Graphics.Point;

namespace MyFinance.Controls;

public partial class PieChart : ContentView
{
    /// <summary>
    /// Вход - список с числами для формирования диаграммы
    ///  
    /// TODO:
    /// 1. Реализовать формирование диаграммы на основе списка значений 
    /// </summary>

    public PieChart()
	{
		InitializeComponent();
        Center = new Point(OuterRadius, OuterRadius);
        Volumes = new() { 3000 };
        //Volumes = new() { 3000, 4000 };
        //Volumes = new() { 3000, 4000, 5000 };
        //Volumes = new() { 3000, 4000, 5000, 1000 };
        //Volumes = new() { 3000, 4000, 5000, 1000, 500 };
        //Volumes = new() { 3000, 4000, 5000, 1000, 500, 1500 };
    }

    private static SolidColorBrush[] Colors { get; } = new SolidColorBrush[] { 
        new(new Color(255, 69, 56)), new(new Color(255, 188, 56)), new(new Color(202, 255, 56)), new(new Color(83, 255, 56)), new(new Color(56, 255, 149)), 
        new(new Color(56, 242, 255)), new(new Color(56, 123, 255)), new(new Color(109, 56, 255)), new(new Color(228, 56, 255)), new(new Color(255, 56, 162)) 
    };
    private double InnerRadius { get; set; } = 80;
    private double OuterRadius { get; set; } = 100;
    private double SumVolumes { get; set; } = 0;
    private Point Center { get; set; }
    private double[] Parts { get; set; }


    public static readonly BindableProperty VolumesProperty = BindableProperty.Create(nameof(Volumes), typeof(List<double>), typeof(PieChart), new List<double>(), propertyChanged: OnVolumesChanged);
    public List<double> Volumes
    {
        get => (List<double>)GetValue(VolumesProperty);
        set => SetValue(VolumesProperty, value);
    }
    private static void OnVolumesChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue is not List<double> newVolumes) return;
        if (newVolumes.Count <= 0) return;
        if (bindable is not PieChart pc) return;
        pc.UpdateParts();
        pc.UpdateLayout();
    }

    private void UpdateParts()
    {
        SumVolumes = Volumes.Sum();
        Parts = new double[Volumes.Count];
        for (int i = 0; i < Volumes.Count; ++i)
            Parts[i] = Volumes[i] / SumVolumes * Math.Tau;
    }

    private void UpdateLayout()
    {
        PathLayout.Children.Clear();
        double prevAngleRadian = 0;
        double curAngleRadian = 0; 
        for (int i = 0, j = 0; i < Parts.Length; ++i, j = j < Colors.Length ? j + 1 : 0)
        {
            curAngleRadian = Parts[i];
            var brush = Colors[j];
            var part = CreatePath(ref prevAngleRadian, ref curAngleRadian, ref brush);
            PathLayout.Children.Add(part);
            prevAngleRadian = curAngleRadian;
        }
    }

    private Path CreatePath(ref double angleStartPartRadian, ref double angleEndPartRadian, ref SolidColorBrush brush)
    {
        var segments = new PathSegmentCollection();
        bool isLargeArc = angleEndPartRadian - angleStartPartRadian > Math.Tau;


        var nextPoint = new Point(
            Center.X + InnerRadius * Math.Cos(angleEndPartRadian),
            Center.Y - InnerRadius * Math.Sin(angleEndPartRadian));
        segments.Add(new ArcSegment
        {
            Point = nextPoint,
            Size = new Size(InnerRadius),
            SweepDirection = SweepDirection.Clockwise
        });

        nextPoint = new Point(
            Center.X + OuterRadius * Math.Cos(angleEndPartRadian),
            Center.Y - OuterRadius * Math.Sin(angleEndPartRadian));
        segments.Add(new LineSegment { Point = nextPoint });


        nextPoint = new Point(
            Center.X + OuterRadius * Math.Cos(angleStartPartRadian),
            Center.Y - OuterRadius * Math.Sin(angleStartPartRadian));
        segments.Add(new ArcSegment
        {
            Point = nextPoint,
            Size = new Size(OuterRadius),
            SweepDirection = SweepDirection.Clockwise
        });

        var data = new PathGeometry();
        data.Figures.Add(new PathFigure
        {
            StartPoint = new Point(
                Center.X + InnerRadius * Math.Cos(angleStartPartRadian),
                Center.Y - InnerRadius * Math.Sin(angleStartPartRadian)),
            Segments = segments,
            IsClosed = true,
            IsFilled = true
        });

        var path = new Path
        {
            Fill = brush,
            Data = data
        };
        return path;
    }
}