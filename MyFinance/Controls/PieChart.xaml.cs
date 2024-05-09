using Microsoft.Maui.Controls.Shapes;
using Color = Microsoft.Maui.Graphics.Color;
using Path = Microsoft.Maui.Controls.Shapes.Path;
using Point = Microsoft.Maui.Graphics.Point;

namespace MyFinance.Controls;

public partial class PieChart : ContentView
{
    public PieChart()
	{
		InitializeComponent();
    }

    private static SolidColorBrush[] Brushes { get; } = new SolidColorBrush[] { 
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
        pc.UpdateSumInfo(pc.SumVolumes.ToString());
        pc.UpdateLayout();
    }


    public static readonly BindableProperty ThicknessPieChartProperty = BindableProperty.Create(nameof(ThicknessPieChart), typeof(double), typeof(PieChart), 25.0, propertyChanged: OnThicknessPieChartChanged);
    public double ThicknessPieChart
    {
        get => (double)GetValue(ThicknessPieChartProperty);
        set => SetValue(ThicknessPieChartProperty, value);
    }
    private static void OnThicknessPieChartChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not PieChart pc) return;
        pc.UpdateLayout();
    }


    public static readonly BindableProperty PeriodInfoProperty = BindableProperty.Create(nameof(PeriodInfo), typeof(string), typeof(PieChart), string.Empty, 
        propertyChanged: OnPeriodInfoChanged);
    public string PeriodInfo
    {
        get => (string)GetValue(PeriodInfoProperty);
        set => SetValue(PeriodInfoProperty, value);
    }
    private static void OnPeriodInfoChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue is not string newPeriodInfo) return;
        if (bindable is not PieChart pc) return;
        pc.UpdatePeriodInfo(newPeriodInfo);
    }

    private void UpdatePeriodInfo(in string info) => InfoPeriod.Text = info;
    private void UpdateSumInfo(in string info) => InfoSum.Text = info;

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
        double prevAngleRadian = 0, curAngleRadian;
        if (Parts is not null && Parts.Length > 1)
        {
            for (int i = 0, j = 0; i < Parts.Length; ++i, j = j < Brushes.Length ? j + 1 : 0)
            {
                curAngleRadian = Parts[i] + prevAngleRadian;
                PathLayout.Children.Add(CreatePath(prevAngleRadian, curAngleRadian, Brushes[j]));
                prevAngleRadian = curAngleRadian;
            }
        }
        else
        {
            PathLayout.Children.Add(CreatePath(0, Math.PI, Brushes[0]));
            PathLayout.Children.Add(CreatePath(Math.PI, Math.Tau, Brushes[0]));
        }
        
    }

    private Path CreatePath(in double angleStartPartRadian, in double angleEndPartRadian, in SolidColorBrush brush)
    {
        var segments = new PathSegmentCollection();
        bool isLargeArc = angleEndPartRadian - angleStartPartRadian > Math.PI;

        segments.Add(Arc(InnerRadius, angleEndPartRadian, isLargeArc, SweepDirection.Clockwise));
        segments.Add(Line(OuterRadius, angleEndPartRadian));
        segments.Add(Arc(OuterRadius, angleStartPartRadian, isLargeArc, SweepDirection.CounterClockwise));

        var data = new PathGeometry();
        data.Figures.Add(new PathFigure
        {
            StartPoint = new Point(
                Center.X + InnerRadius * Math.Sin(angleStartPartRadian),
                Center.Y - InnerRadius * Math.Cos(angleStartPartRadian)),
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

    private ArcSegment Arc(in double radius, in double anglePartRadian, in bool isLargeArc, in SweepDirection direction) => new()
    {
        Point = NextPoint(radius, anglePartRadian),
        Size = new Size(radius),
        IsLargeArc = isLargeArc,
        SweepDirection = direction
    };

    private LineSegment Line(in double radius, in double anglePartRadian) => new() { Point = NextPoint(radius, anglePartRadian) };

    private Point NextPoint(in double radius, in double anglePartRadian) => new(Center.X + radius * Math.Sin(anglePartRadian), Center.Y - radius * Math.Cos(anglePartRadian));

    private void ContentView_SizeChanged(object sender, EventArgs e)
    {
        OuterRadius = Width > Height ? Height / 2 : Width / 2;
        InnerRadius = OuterRadius - ThicknessPieChart;
        Center = new Point(OuterRadius, OuterRadius);
        UpdateLayout();
        PathLayout.Margin = new Thickness(Width / 2 - OuterRadius, Height / 2 - OuterRadius);
    }
}