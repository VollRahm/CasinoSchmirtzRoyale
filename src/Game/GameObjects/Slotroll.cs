using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Casino_Schmirtz_Royale.Game.GameObjects
{
    public class Slotroll
    {
        private const int topHidden = -120;
        private const int firstPosition = 30;
        private const int secondPosition = 180;
        private const int thirdPosition = 330;
        private const int bottomHidden = 481;

        private Canvas rollCanvas;

        private SlotrollElement newRoll;
        private SlotrollElement topRoll;
        private SlotrollElement middleRoll;
        private SlotrollElement bottomRoll;

        public Slotroll(Canvas canv)
        {
            rollCanvas = canv;
        }

        public void Initialize()
        {
            newRoll = new SlotrollElement(RandomSymbol(), topHidden);
            topRoll = new SlotrollElement(RandomSymbol(), firstPosition);
            middleRoll = new SlotrollElement(RandomSymbol(), secondPosition);
            bottomRoll = new SlotrollElement(RandomSymbol(), thirdPosition);

            rollCanvas.Children.Add(newRoll.element);
            rollCanvas.Children.Add(topRoll.element);
            rollCanvas.Children.Add(middleRoll.element);
            rollCanvas.Children.Add(bottomRoll.element);
            
            
        }

        private SlotSymbol RandomSymbol()
        {
            int jackpot = GameManager.rnd.Next(0, 10);
            if(jackpot == 4)
            {
                return SlotSymbol.Money;
            }

            int num = GameManager.rnd.Next(0, 4);
            return (SlotSymbol)num;
        }

        public async Task<SlotSymbol> Spin(int times)
        {
            for (int i = 0; i < times; i++)
            {
                _ = newRoll.MoveTo(firstPosition);
                _ = topRoll.MoveTo(secondPosition);
                _ = middleRoll.MoveTo(thirdPosition);
                await bottomRoll.MoveTo(bottomHidden);
                var toDelete = bottomRoll;
                bottomRoll = middleRoll;
                middleRoll = topRoll;
                topRoll = newRoll;

                rollCanvas.Children.Remove(toDelete.element);

                newRoll = new SlotrollElement(RandomSymbol(), topHidden);
                rollCanvas.Children.Add(newRoll.element);

               
            }
            return middleRoll.symbol;
        }

        bool spinning = false;
        Task spinTask;

        public void StartSpin()
        {
            spinning = true;
            spinTask = SpinContiniously();
        }

        private async Task SpinContiniously()
        {
            while (spinning)
            {
                await Spin(1);
            }
        }

        public async Task StopSpin()
        {
            spinning = false;
            await spinTask;
        }

    }

    public enum SlotSymbol
    {
        Ace,
        Cherry,
        Chip,
        Heart,
        Money
    }
}
