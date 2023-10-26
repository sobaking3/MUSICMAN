using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToastNotifications.Core;
using ToastNotifications.Position;

namespace MUSICMAN.ClassFolder
{
    public class WindowPositionProviderCustom : IPositionProvider, IDisposable
    {
        private readonly Corner _corner;

        private readonly double _offsetX;

        private readonly double _offsetY;

        public Window ParentWindow { get; }

        public Func<int, Window> ParentWindow2 { get; set; }

        public EjectDirection EjectDirection { get; private set; }

        public event EventHandler UpdatePositionRequested;

        public event EventHandler UpdateEjectDirectionRequested;

        public event EventHandler UpdateHeightRequested;

        public WindowPositionProviderCustom(Func<int, Window> parentWindow, Corner corner, double offsetX, double offsetY)
        {
            
            _corner = corner;
            _offsetX = offsetX;
            _offsetY = offsetY;
            ParentWindow2 = parentWindow;
            ParentWindow2(1).SizeChanged += ParentWindow2OnSizeChanged;
            ParentWindow2(1).LocationChanged += ParentWindow2OnLocationChanged;
            ParentWindow2(1).StateChanged += ParentWindow2OnStateChanged;
            ParentWindow2(1).Activated += ParentWindow2OnActivated;
            SetEjectDirection(corner);
        }

        public Point GetPosition(double actualPopupWidth, double actualPopupHeight)
        {
            Point actualPosition = ParentWindow2(1).GetActualPosition();

            switch(_corner)
            {
                case Corner.TopRight:
                    return GetPositionForTopRightCorner(actualPosition, actualPopupWidth, actualPopupHeight);
                    
                case Corner.TopLeft:
                    return GetPositionForTopLeftCorner(actualPosition, actualPopupWidth, actualPopupHeight);
                case Corner.BottomRight:
                    return GetPositionForBottomRightCorner(actualPosition, actualPopupWidth, actualPopupHeight);
                case Corner.BottomLeft:
                    return GetPositionForBottomLeftCorner(actualPosition, actualPopupWidth, actualPopupHeight);
                case Corner.BottomCenter:
                    return GetPositionForBottomCenter(actualPosition, actualPopupWidth, actualPopupHeight);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public double GetHeight()
        {
            return (ParentWindow2(1).Content as FrameworkElement)?.ActualHeight ?? ParentWindow2(1).ActualHeight;
        }

        private double GetWindowWidth()
        {
            return (ParentWindow2(1).Content as FrameworkElement)?.ActualWidth ?? ParentWindow2(1).ActualWidth;
        }

        private void SetEjectDirection(Corner corner)
        {
            switch (corner)
            {
                case Corner.TopRight:
                case Corner.TopLeft:
                    EjectDirection = EjectDirection.ToBottom;
                    break;
                case Corner.BottomRight:
                case Corner.BottomLeft:
                case Corner.BottomCenter:
                    EjectDirection = EjectDirection.ToTop;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("corner", corner, null);
            }
        }

        private Point GetPositionForBottomLeftCorner(Point parentPosition, double actualPopupWidth, double actualPopupHeight)
        {
            return new Point(parentPosition.X + _offsetX, parentPosition.Y - _offsetY);
        }

        private Point GetPositionForBottomRightCorner(Point parentPosition, double actualPopupWidth, double actualPopupHeight)
        {
            return new Point(parentPosition.X + GetWindowWidth() - _offsetX - actualPopupWidth, parentPosition.Y - _offsetY);
        }

        private Point GetPositionForBottomCenter(Point parentPosition, double actualPopupWidth, double actualPopupHeight)
        {
            return new Point(parentPosition.X + (GetWindowWidth() - actualPopupWidth) / 2.0, parentPosition.Y - _offsetY);
        }

        private Point GetPositionForTopLeftCorner(Point parentPosition, double actualPopupWidth, double actualPopupHeight)
        {
            return new Point(parentPosition.X + _offsetX, parentPosition.Y + _offsetY);
        }

        private Point GetPositionForTopRightCorner(Point parentPosition, double actualPopupWidth, double actualPopupHeight)
        {
            return new Point(parentPosition.X + GetWindowWidth() - _offsetX - actualPopupWidth, parentPosition.Y + _offsetY);
        }

        public void Dispose()
        {
            ParentWindow2(1).LocationChanged -= ParentWindow2OnLocationChanged;
            ParentWindow2(1).SizeChanged -= ParentWindow2OnSizeChanged;
            ParentWindow2(1).StateChanged -= ParentWindow2OnStateChanged;
            ParentWindow2(1).Activated -= ParentWindow2OnActivated;
        }

        protected virtual void RequestUpdatePosition()
        {
            this.UpdateHeightRequested?.Invoke(this, EventArgs.Empty);
            this.UpdateEjectDirectionRequested?.Invoke(this, EventArgs.Empty);
            this.UpdatePositionRequested?.Invoke(this, EventArgs.Empty);
        }

        private void ParentWindow2OnLocationChanged(object sender, EventArgs eventArgs)
        {
            RequestUpdatePosition();
        }

        private void ParentWindow2OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            RequestUpdatePosition();
        }

        private void ParentWindow2OnStateChanged(object sender, EventArgs eventArgs)
        {
            RequestUpdatePosition();
        }

        private void ParentWindow2OnActivated(object sender, EventArgs eventArgs)
        {
            RequestUpdatePosition();
        }
    }
}
