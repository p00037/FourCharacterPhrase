using FourCharacterPhrase.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace FourCharacterPhrase.WPF
{
    class CellStatusToBackgroundConverter : IValueConverter
    {
        /// <summary>
        /// cellStatusをBackGroundの値に変換
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 変換元データを変数に受け取る。
            var cellStatus = (CellStatus)value;

            switch (cellStatus)
            {
                case CellStatus.None:
                    return new SolidColorBrush(Colors.White);
                case CellStatus.Selecting:
                    return new SolidColorBrush(Colors.SkyBlue);
                case CellStatus.Completed:
                    return new SolidColorBrush(Colors.DimGray);
                default:
                    return new SolidColorBrush(Colors.White);
            }
        }

        /// <summary>
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
