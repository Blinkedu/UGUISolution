﻿using UnityEngine;
using UnityEngine.UI;

namespace XCharts
{
    /// <summary>
    /// DataZoom component is used for zooming a specific area,
    /// which enables user to investigate data in detail,
    /// or get an overview of the data, or get rid of outlier points.
    /// </summary>
    [System.Serializable]
    public class DataZoom
    {
        public enum DataZoomType
        {
            /// <summary>
            /// DataZoom functionalities is embeded inside coordinate systems, enable user to 
            /// zoom or roam coordinate system by mouse dragging, mouse move or finger touch (in touch screen).
            /// </summary>
            Inside,
            /// <summary>
            /// A special slider bar is provided, on which coordinate systems can be zoomed or
            /// roamed by mouse dragging or finger touch (in touch screen).
            /// </summary>
            Slider
        }

        /// <summary>
        /// Generally dataZoom component zoom or roam coordinate system through data filtering
        /// and set the windows of axes internally.
        /// Its behaviours vary according to filtering mode settings
        /// </summary>
        public enum FilterMode
        {
            /// <summary>
            /// data that outside the window will be filtered, which may lead to some changes of windows of other axes.
            /// For each data item, it will be filtered if one of the relevant dimensions is out of the window.
            /// </summary>
            Filter,
            /// <summary>
            /// data that outside the window will be filtered, which may lead to some changes of windows of other axes.
            /// For each data item, it will be filtered only if all of the relevant dimensions are out of the same side of the window.
            /// </summary>
            WeakFilter,
            /// <summary>
            /// data that outside the window will be set to NaN, which will not lead to changes of windows of other axes
            /// </summary>
            Empty,
            /// <summary>
            /// Do not filter data.
            /// </summary>
            None
        }
        public enum RangeMode
        {
            //Value,
            /// <summary>
            /// percent value
            /// </summary>
            Percent
        }
        [SerializeField] private bool m_Show;
        [SerializeField] private DataZoomType m_Type;
        [SerializeField] private FilterMode m_FilterMode;
        [SerializeField] private Orient m_Orient;
        [SerializeField] private int m_XAxisIndex;
        [SerializeField] private int m_YAxisIndex;

        [SerializeField] private bool m_ShowDataShadow;
        [SerializeField] private bool m_ShowDetail;
        [SerializeField] private bool m_ZoomLock;
        [SerializeField] private bool m_Realtime;
        [SerializeField] private Color m_BackgroundColor;
        [SerializeField] private float m_Height;
        [SerializeField] private float m_Bottom;
        [SerializeField] private RangeMode m_RangeMode;
        [SerializeField] private float m_Start;
        [SerializeField] private float m_End;
        [SerializeField] private float m_StartValue;
        [SerializeField] private float m_EndValue;
        [Range(1f, 20f)]
        [SerializeField] private float m_ScrollSensitivity;

        public bool show { get { return m_Show; } set { m_Show = value; } }
        public DataZoomType type { get { return m_Type; } set { m_Type = value; } }
        public FilterMode filterMode { get { return m_FilterMode; } set { m_FilterMode = value; } }
        /// <summary>
        /// Specify whether the layout of dataZoom component is horizontal or vertical. 
        /// </summary>
        public Orient orient { get { return m_Orient; } set { m_Orient = value; } }
        public int xAxisIndex { get { return m_XAxisIndex; } set { m_XAxisIndex = value; } }
        public int yAxisIndex { get { return m_YAxisIndex; } set { m_YAxisIndex = value; } }
        /// <summary>
        /// Whether to show data shadow, to indicate the data tendency in brief.
        /// default:true
        /// </summary>
        public bool showDataShadow { get { return m_ShowDataShadow; } set { m_ShowDataShadow = value; } }
        /// <summary>
        /// Whether to show detail, that is, show the detailed data information when dragging.
        /// </summary>
        /// <value></value>
        public bool showDetail { get { return m_ShowDetail; } set { m_ShowDetail = value; } }
        /// <summary>
        /// Specify whether to lock the size of window (selected area).
        /// default:false
        /// </summary>
        public bool zoomLock { get { return m_ZoomLock; } set { m_ZoomLock = value; } }
        /// <summary>
        /// Whether to show data shadow in dataZoom-silder component, to indicate the data tendency in brief.
        /// default:true
        /// </summary>
        public bool realtime { get { return m_Realtime; } set { m_Realtime = value; } }
        /// <summary>
        /// The background color of the component.
        /// </summary>
        public Color backgroundColor { get { return m_BackgroundColor; } set { m_BackgroundColor = value; } }
        /// <summary>
        /// Distance between dataZoom component and the bottom side of the container.
        /// bottom value is a instant pixel value like 10.
        /// default:10
        /// </summary>
        public float bottom { get { return m_Bottom; } set { m_Bottom = value; } }
        /// <summary>
        /// The height of dataZoom component.
        /// height value is a instant pixel value like 10.
        /// default:50
        /// </summary>
        public float height { get { return m_Height; } set { m_Height = value; } }
        /// <summary>
        /// Use absolute value or percent value in DataZoom.start and DataZoom.end.
        /// default:RangeMode.Percent
        /// </summary>
        public RangeMode rangeMode { get { return m_RangeMode; } set { m_RangeMode = value; } }
        /// <summary>
        /// The start percentage of the window out of the data extent, in the range of 0 ~ 100.
        /// default:30
        /// </summary>
        public float start { get { return m_Start; } set { m_Start = value; } }
        /// <summary>
        /// The end percentage of the window out of the data extent, in the range of 0 ~ 100.
        /// default:70
        /// </summary>
        public float end { get { return m_End; } set { m_End = value; } }
        /// <summary>
        /// The sensitivity of dataZoom scroll.
        /// The larger the number, the more sensitive it is.
        /// default:10
        /// </summary>
        public float scrollSensitivity { get { return m_ScrollSensitivity; } set { m_ScrollSensitivity = value; } }

        /// <summary>
        /// DataZoom is in draging.
        /// </summary>
        public bool isDraging { get; set; }
        /// <summary>
        /// The start label.
        /// </summary>
        public Text startLabel { get; set; }
        /// <summary>
        /// The end label.
        /// </summary>
        public Text endLabel { get; set; }

        public static DataZoom defaultDataZoom
        {
            get
            {
                return new DataZoom()
                {
                    m_Type = DataZoomType.Slider,
                    filterMode = FilterMode.None,
                    orient = Orient.Horizonal,
                    xAxisIndex = 0,
                    yAxisIndex = 0,
                    showDataShadow = true,
                    showDetail = false,
                    zoomLock = false,
                    realtime = true,
                    m_Height = 50,
                    m_Bottom = 10,
                    rangeMode = RangeMode.Percent,
                    start = 30,
                    end = 70,
                    m_ScrollSensitivity = 10,
                };
            }
        }

        public bool IsInZoom(Vector2 pos, float startX, float width)
        {
            Rect rect = Rect.MinMaxRect(startX, m_Bottom, startX + width, m_Bottom + m_Height);
            return rect.Contains(pos);
        }

        public bool IsInSelectedZoom(Vector2 pos, float startX, float width)
        {
            var start = startX + width * m_Start / 100;
            var end = startX + width * m_End / 100;
            Rect rect = Rect.MinMaxRect(start, m_Bottom, end, m_Bottom + m_Height);
            return rect.Contains(pos);
        }

        public bool IsInStartZoom(Vector2 pos, float startX, float width)
        {
            var start = startX + width * m_Start / 100;
            Rect rect = Rect.MinMaxRect(start - 10, m_Bottom, start + 10, m_Bottom + m_Height);
            return rect.Contains(pos);
        }

        public bool IsInEndZoom(Vector2 pos, float startX, float width)
        {
            var end = startX + width * m_End / 100;
            Rect rect = Rect.MinMaxRect(end - 10, m_Bottom, end + 10, m_Bottom + m_Height);
            return rect.Contains(pos);
        }

        public void SetLabelActive(bool flag)
        {
            if (startLabel && startLabel.gameObject.activeInHierarchy != flag)
            {
                startLabel.gameObject.SetActive(flag);
            }
            if (endLabel && endLabel.gameObject.activeInHierarchy != flag)
            {
                endLabel.gameObject.SetActive(flag);
            }
        }

        public void SetStartLabelText(string text)
        {
            if (startLabel) startLabel.text = text;
        }

        public void SetEndLabelText(string text)
        {
            if (endLabel) endLabel.text = text;
        }
    }
}