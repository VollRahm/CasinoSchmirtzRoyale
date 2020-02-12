using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Casino_Schmirtz_Royale.Game.GameObjects
{
    public class SlotrollElement
    {
        public Rectangle element;
        public SlotSymbol symbol;

        private int pos;

        public SlotrollElement(SlotSymbol slotSymbol, int position)
        {
            element = new Rectangle();
            element.Height = 120;
            element.Width = 120;
            string imgName;
            Uri imgUri;
            switch (slotSymbol)
            {
                case SlotSymbol.Ace:
                    imgName = "ace.png";
                    break;
                case SlotSymbol.Cherry:
                    imgName = "cherry.png";
                    break;
                case SlotSymbol.Chip:
                    imgName = "chip.png";
                    break;
                case SlotSymbol.Heart:
                    imgName = "heart.png";
                    break;
                case SlotSymbol.Money:
                    imgName = "money.png";
                    break;
                default:
                    throw new Exception("file not found or something like that");
            }
            imgUri = new Uri(@"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/"+ "Assets/RollImages/"+imgName, UriKind.Absolute);

            element.Fill = new ImageBrush(new BitmapImage(imgUri));
            Canvas.SetLeft(element, 130);
            Canvas.SetTop(element, position);

            this.symbol = slotSymbol;
            this.pos = position;
        }

        public async Task MoveTo(int position)
        {
            while(pos != position)
            {
                if(position -pos < 20)
                {
                    Canvas.SetTop(element, position);
                    pos = position;
                    return;
                }
                Canvas.SetTop(element, pos + 20);
                pos+=20;
                await Task.Delay(5);
            }
        }
    }
}
