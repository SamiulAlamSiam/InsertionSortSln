using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InsertionSort
{
    public enum StepsEnum
    {
        Start = 0,
        Selection = 1,
        Comparison = 2,
        Success = 3,
        Failure = 4
    }

    public partial class Form1 : Form
    {
        private int w = 15, l = 185;
        private int i = 0, j = 0;

        int sortedIndex=0;
        int index = 0;
        private int count = 0;
        private int t = 0;
        private int prevX = 0;

        List<Button> allBtns = new List<Button>();
        List<Button> sortedBtns = new List<Button>();
        List<Button> reverseBtns = new List<Button>();
        List<int> locationX = new List<int>();
        List<int> locationY = new List<int>(); 
        private bool test = true;

        Button firstButton, secondButton;
        

        private StepsEnum currentStep;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            currentStep = StepsEnum.Start;
            i = j = -1;
            foreach (var item in lblArrayItem.Items)
            {
                Button btn = new Button();
                btn.Text = item.ToString();
                btn.Location = new Point(w, l);
                btn.Size = new Size(75, 25);
                btn.BackColor = Color.DarkCyan;
                btn.ForeColor = Color.White;

                this.panel1.Controls.Add(btn);
                allBtns.Add(btn);
                //sortedBtns.Add(btn);

                w += 85;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            for (int k = 0; k < allBtns.Count; k++)
            {
                allBtns[k].BackColor = Color.DarkCyan;
            }

            if (currentStep == StepsEnum.Start)
                Start();
            else if (currentStep == StepsEnum.Selection)
                Selection();
            else if (currentStep == StepsEnum.Comparison)
                CompareValue();
            else if (currentStep == StepsEnum.Failure)
                Failure();
            else if (currentStep == StepsEnum.Success)
                Success();
        }

        

        private void Success()
        {
            if (sortedBtns.Count != 0 && locationX.Count != 0)
            {
                sortedBtns.Clear();
                locationX.Clear();
                j = 0;
            }
           
            for (int k = 0; k < allBtns.Count; k++)
            {
                if (index > sortedIndex)
                {
                    firstButton = allBtns[index - 1];
                    secondButton = allBtns[index];

                    sortedBtns.Add(firstButton);

                    prevX = firstButton.Location.X;

                    Button temp;
                    temp = allBtns[index];
                    allBtns[index] = allBtns[index - 1];
                    allBtns[index - 1] = temp;

                    

                    count = 1;
                }
                else
                {
                    break;
                }

                index--;
            }

            sortedIndex++;
            currentStep = StepsEnum.Selection;

            sortedBtns.Reverse();
            sortedBtns.Add(secondButton);

            foreach (var sortedBtn in sortedBtns)
            {
                locationX.Add(sortedBtn.Location.X);
                locationY.Add(sortedBtn.Location.Y);
            }

            timer1.Start();
            btnNext.Enabled = false;
            //Selection();
        }

        private void Failure()
        {
            sortedIndex++;
            currentStep = StepsEnum.Selection;
            Selection();
            allBtns[index].BackColor = Color.DarkRed;
        }

        private void CompareValue()
        {
            if (index == sortedIndex)
            {
                currentStep = StepsEnum.Failure;
                allBtns[index].BackColor = Color.DarkRed;
            }

            else 
            {
                currentStep = StepsEnum.Success;
                allBtns[index].BackColor = Color.DarkGreen;
            }
        }

        private void Selection()
        {
            btnNext.Enabled = true;
            index = sortedIndex;

            if (sortedIndex == allBtns.Count-1)
            {
                btnNext.Enabled = false;
            }

            int min = Int32.Parse(allBtns[sortedIndex].Text);

            
            for (int i = sortedIndex; i < allBtns.Count; i++)
            {
                if (min > Int32.Parse(allBtns[i].Text))
                {
                    t = 1;
                    index = i;
                    min = Int32.Parse(allBtns[i].Text);
                }
                    
            }

            allBtns[index].BackColor = Color.DarkBlue;

            currentStep = StepsEnum.Comparison;            

            //if(sortedIndex>0)
            //    allBtns.RemoveAt(sortedIndex-1);
        }

        private void Start()
        {
            
            currentStep = StepsEnum.Selection;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(count!=2)
                i = sortedBtns.Count - 2;
                if (j < 5)
                {
                    secondButton.Location = new Point(secondButton.Location.X, secondButton.Location.Y - 5);
                    j++;
                    count = 1;
                }

                else if (i < 0)
                {
                    timer1.Stop();
                    btnNext.Enabled = true;
                }

                else if (secondButton.Location.X > locationX[0])
                {
                    secondButton.Location = new Point(secondButton.Location.X - 5, secondButton.Location.Y);
                }

                else if (sortedBtns[i].Location.X > locationX[i + 1])
                {
                    count = 2;
                    i--;
                }

                else if (secondButton.Location.Y < locationY[0] && sortedBtns[0].Location.X > locationX[0] + 84)
                {
                    secondButton.Location = new Point(secondButton.Location.X, secondButton.Location.Y + 5);
                }

                else 
                {
                    sortedBtns[i].Location = new Point(sortedBtns[i].Location.X + 5, sortedBtns[i].Location.Y);
                    count = 1;
                }     
        }
    }
}
