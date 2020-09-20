/*************************************************
//Name: Faiza Yahya
//Assignment: Lab #1
//Instructor: Shane
//Main Funtionality: Solve the maze by using recursion.
//The steps counts the number of steps till the solution.
//If the speed is increased the maze draws slowly.
//If the maze are larger we pass it through threading. 
//We increase the size of stack by 20MB
//*************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GDIDrawer;
using System.IO;
using System.Threading;

namespace Maze_Solver
{
    //create a delegate to update the maze
    delegate void delVoidVoid(int steps, bool solved_);
    public partial class Form1 : Form
    {
        //All events must be explicitly wired up in the form constructor.
        //A CDrawer object, initialized to null ( a new one will be made for each maze ) 
        static CDrawer canvas = null; 
        public const int height_ = 600;
        public const int width_ = 800;
        bool stop = false;
      
        //create a structure for Point
        public struct Point_Val
        {
            public Point pointVal_;
        }

        //declare a thread at the class scope
        Thread maze_thread;

        /*Make a structure called MazeInfo, containing the following fields :
         * a) a Point - representing the start of our maze 
         * b) a Point - representing the end of our maze
         * c) an integer - representing the width ( columns ) of our maze 
         * d) an integer - representing the height ( rows ) of our maze
         * e) a Color - representing the color of a solution path
         * f) a Color - representing the color of a dead end path
         * g) an integer - representing the number of steps taken to solve our maze
         * h) a bool - representing whether the maze has been solved
         * */
        public struct MazeInfo
        {
            public Point p_start;
            public Point p_end;
            public int width;
            public int height;
            public Color Sol_color;
            public Color Dead_color;
            public int steps;
            public bool solved;
        }
        //file string
        string[] filename_;

        // an enumeration indicating open, wall or visited 
        enum Obstacle { open, wall, visited };
        // a MazeInfo member
        MazeInfo MazeInfo_struct; 
        // a 2D array of your enumeration
        Obstacle[,] Obs_array;

        public Form1()
        {
            InitializeComponent();
            //The Solve button should only be enabled if a maze 
            //has been successfully loaded
            buttonSolve.Enabled = false;
        }

        /*If the Load button is clicked then, the maze is loaded on the
          screen*/
        private void buttonLoadMaze_Click(object sender, EventArgs e)
        {
            Bitmap bm = new Bitmap(500, 600);

            //Create a new OpenFileDialog(not on the form * ) then setting : 
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
           
            // the initial directory to the current directory and up 2 levels ( ..\ )
            string directory = Path.GetFullPath(Path.Combine(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString(), @"..\..\"));
            ofd.InitialDirectory = directory;
          
            //set the title bar to “Load Bitmap maze to solve”
            this.Text = "Load Bitmap maze to solve";

            // filter to show two groups, Bitmaps, and All Files 
            ofd.Filter = "BMP|*.bmp";
            ofd.Filter = "BMP|*.bmp|" + "All Files|*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = null;

                //perform error checking in opening the picture file
                try
                {
                    //read from the file
                    sr = new StreamReader(ofd.FileName);
                    //Utilizing the Bitmap load file snippet in the CDrawer manual, 
                    //attempt to load the bitmap
                    bm = new Bitmap(ofd.FileName);

                    /* include 2 catches, one for a load failure ( as illustrated in the bitmap load snippet ) 
                     * //and an additional generic Exception catch for your width/height constraint,
                     * //both should display a Message box indicating the specified error
                     * */
                    //If the bitmap width exceeds 190 or height exceeds 100  throw a new Exception()
                    //with the message “Bitmap size exceeds displayable area” ( caught later ) 
                    /*  if (bm.Height > 100 || bm.Width > 190)
                      {
                          throw new System.ArgumentException("Bitmap size exceeds displayable area", "(caught later)");
                          MessageBox.Show("Bitmap size exceeds displayable area", "Error !", MessageBoxButtons.OK);
                      }
                    */

                    //set the member maximum column and row to their respective 
                    //bitmap width and height values
                    MazeInfo_struct.height = bm.Height;
                    MazeInfo_struct.width = bm.Width;
                }
                //if the operation is not sucessful throw an error
                catch (Exception message)
                {
                    MessageBox.Show("Error opening the file: ", "Error !", MessageBoxButtons.OK);
                }
                finally
                {
                    //close the file
                    sr.Close();
                }

                //If your current Drawer object is not null, Close() it, we are done, 
                //and need a new one for the new maze.
                if (canvas != null)
                  canvas.Close();

                this.Text = ofd.FileName;

                //call the function to draw the image
                CreateCanvas(bm, this.Text);
              

            }
        }

        /*******************************************************
         * Funtion:   private void CreateCanvas(Bitmap bm)
         * Parameters: Bitmap bm
         * Returns : void
         * Funtionality: It Loads and scaled the new bitmap image on canvas
         * *****************************************************/
        private void CreateCanvas(Bitmap bm, string filename)
        {
            
            //set the appropriate scale
            // You are required to adjust the drawer scale value to allow for the
            //mazes to fit on the screen ( after placement ). If a scale of 1 still won't fit,
            //you may reject the maze at load time.
            int scale;
            if ((bm.Height < 50) && (bm.Width < 50))
                scale = 10;
           else if ((bm.Height < 100) && (bm.Width < 100))
                scale = 6;
            else if (bm.Height < 100 && bm.Width < 200)
                scale = 5;
            else if ((bm.Height >= 100 && bm.Height < 250) && (bm.Width >= 100 && bm.Width < 250))
                scale = 4;
            else if ((bm.Height >= 250 && bm.Height <= 300) && (bm.Width >= 250 && bm.Width < 500))
                scale = 2;
            else
                scale = 1;



           // allocate a new CDrawer of the appropriate size(ie.If bitmap is 40x30, then allocate( 40 * 10, 30 * 10 ) ).
            canvas = new CDrawer(MazeInfo_struct.width * scale, MazeInfo_struct.height * scale);//create the GDI Drawer
            canvas.Scale = scale;
            canvas.Render();
            //turn continuous update to false 
            canvas.ContinuousUpdate = false;

            //and the background to white.
            canvas.BBColour = Color.White;
            canvas.Render();

            //create a new instance of the 2D array
            Obs_array = new Obstacle[MazeInfo_struct.width, MazeInfo_struct.height];
            for (int row = 0; row <MazeInfo_struct.height; row++)
            {
                for (int col = 0; col <MazeInfo_struct.width; col++)
                {
                    //draw the image onto the canvas
                    //use SetBBScaledPixel() of the drawer to populate the drawer background with our maze
                    canvas.SetBBScaledPixel(col, row, bm.GetPixel(col, row));
                    canvas.Render();//render to show

                    /* assign your member maze array to a new 2D array of enumeration of dimensions [ maxCol, maxRow ], 
                     * //as this will internally represent our maze for processing. 
                     * //Our premise will be :  value open = open spot to explore 
                     * //value wall = wall, can't go there 
                     * //value visited = been there, done that, don't go */
                    if (bm.GetPixel(col, row) == Color.FromArgb(0, 0, 0))
                        Obs_array[col, row] = Obstacle.wall;//if the color is black * set that array location in our maze array = 1 ( wall ) 
                    else if (bm.GetPixel(col, row) == Color.FromArgb(255, 255, 255))
                        Obs_array[col, row] = Obstacle.open;// if the color is white, set that array location in our maze array = 0 ( open )
                   // if the color is green, set the start point member to this X / Y, Col / 
                   //Row location indicating the maze start location
                    else if (bm.GetPixel(col, row) == Color.FromArgb(0, 255, 0))
                    {
                        MazeInfo_struct.p_start.X = col;
                        MazeInfo_struct.p_start.Y = row;;
                    }
                    // if the color is red, set the start 
                    //point member to this X / Y, Col / Row location indicating the maze end location 
                    else if (bm.GetPixel(col, row) == Color.FromArgb(255, 0, 0))
                    {
                        MazeInfo_struct.p_end.X = col;
                        MazeInfo_struct.p_end.Y = row;
                    }             
               }
            }
            //file is loaded set the enabled to true and able the button solve
            buttonSolve.Enabled = true;
            filename_ =filename.Split('\\');
            listBoxData.Items.Add($"Loaded: {filename_[7]}");

        }

        //lets you choose the solve color
        private void buttonSolveColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                MazeInfo_struct.Sol_color = colorDialog1.Color;
                buttonSolveColor.BackColor = colorDialog1.Color;
            }

        }

        /* ********************************************************************
         * Funtion:  private MazeInfo Recursive_Method(Point p_, MazeInfo maze_s)
         *Parameters: Point p_, MazeInfo maze_s
         *Return: Maze info object that indicates maze is solved or not
         *Funtionality: Solves the maze recursively if the dead end is reached it backtracks and showing a backtracking 
         *in a different color
         ***********************************************************************/
        private MazeInfo Recursive_Method(Point p_, MazeInfo maze_s)
        {
            if (stop == true)
            {
                maze_s.solved = true;
                return maze_s;
            }
            //check if the point is out of bounds if it is return
            if (p_.X >= canvas.ScaledWidth || p_.X < 0 || p_.Y >= canvas.ScaledHeight || p_.Y < 0)
            {
                maze_s.solved = false;
                return maze_s;
            }

            //check if the point has reached the end if it is return and set solved to true
            if (p_ == MazeInfo_struct.p_end)
            {
                maze_s.solved = true;
                return maze_s;
            }
            //if there is obstacle wall or the obstacle has been visited set the maze solved to false
            if ((Obs_array[p_.X, p_.Y] == Obstacle.wall) || ((Obs_array[p_.X, p_.Y] == Obstacle.visited)))
            {
                maze_s.solved = false;
                return maze_s;
            }

            //increase the steps or count the total number of steps it took to solve the masze
            maze_s.steps++;

            //if we are checking the point that means it has been visited
            Obs_array[p_.X, p_.Y] = Obstacle.visited;

            //set the speed 
            if (p_ != MazeInfo_struct.p_end && p_ != MazeInfo_struct.p_start)
            {
                if (numericUpDownSpeed.Value > 0)
                {
                    maze_s.solved = true;
                    int speed = (int)numericUpDownSpeed.Value * 10;
                    Thread.Sleep(speed);
                }

            }

            //If it is not equal to start draw the correct solution
            if (p_ != maze_s.p_start)
            {
                canvas.SetBBScaledPixel(p_.X, p_.Y, maze_s.Sol_color);
                canvas.Render();
            }

            //recurse in all directions if the maze is solved set the flag to solved
            //and return
            //checks x+1 direction  if maze is solved true is returned
            maze_s = Recursive_Method(new Point(p_.X + 1, p_.Y), maze_s);

            if (maze_s.solved)
            {              
                maze_s.solved = true;
                return maze_s;
            }
            //checks the x -1 direction if maze is solved true is returned
            maze_s = Recursive_Method(new Point(p_.X - 1, p_.Y), maze_s);
            if (maze_s.solved)
            {
                maze_s.solved = true;
                return maze_s;
            }


            //checks y +1 direction  if maze is solved true is returned
            maze_s = Recursive_Method(new Point(p_.X, p_.Y + 1), maze_s);
            if (maze_s.solved)
            {
                maze_s.solved = true;
                return maze_s;
            }

            //checks y -1 direction  if maze is solved true is returned
            maze_s = Recursive_Method(new Point(p_.X, p_.Y - 1), maze_s);
            if (maze_s.solved)
            {
                maze_s.solved = true;
                return maze_s;
            }

            //if no solution was found that means there is a dead end.
            //show the dead end in a dead end color
            //this is basically backtracking.
           
            if (p_ != maze_s.p_start)
            {
                canvas.SetBBScaledPixel(p_.X, p_.Y, maze_s.Dead_color);
                canvas.Render();
            }
            
            //no solution was found return false
            maze_s.solved = false;
            return maze_s;


        }

        /* Calls the recursive Funtions to solve the required maze
         *  comprised of 2 methods, the event handler for the button and an additional recursive method that will
         *  actually solve the maze.  the event handler will do nothing more than call your recursive method
         *  /passing MazeInfo object and the current maze point,
         *  the return value of type MazeInfo indicates that the 
         *  Solve attempt is complete – true=success, false=no solution and the number of steps to the exit
          */
        private void buttonSolve_Click(object sender, EventArgs e)
        {
            stop = false;

            string[] filename = filename_[7].Split('_');
            //indicate a point struct
            Point_Val Point_struct ;
            Point_struct.pointVal_ = new Point(0, 0);

            //set the steps to zero
            MazeInfo_struct.steps = 0;
            //assign the initial start points to the pount value
            Point_struct.pointVal_ = MazeInfo_struct.p_start;
            //set the solved to false
             MazeInfo_struct.solved = false;          
            if (buttonSolve.Text == "Solve")
            {
               
                //For implementation, provide 2 paths to maze solutions.\
                //If the maze overall area is larger than 4000 or if the current sleep value is greater than 4,
                //use the threaded solver, 
                //otherwise directly solve the maze without a thread.
                if ((MazeInfo_struct.height * MazeInfo_struct.width < 4000) && numericUpDownSpeed.Value <= 4)
                {
                  
                    MazeInfo maze_s = ((Recursive_Method(Point_struct.pointVal_, MazeInfo_struct)));
                    if (maze_s.solved == true)
                    {
                        listBoxData.Items.Add($"Solved in {maze_s.steps}");
                    }
                    else
                    {
                        listBoxData.Items.Add($"Maze is not solved!");
                    }
                }
                else
                {
                    try
                    {
                        listBoxData.Items.Add($"We are now using threading to solve the maze. ");
                        listBoxData.Items.Add($"Attempting threaded solve of maze {filename[1]} maze ");
                        if (numericUpDownSpeed.Value >= 1)
                        {
                            stop = false;
                            buttonSolve.Text = "Cancel";
                        }
                        //Increase the maximize stack size in a thread spawn ( 2nd argument overload ),
                        //to 20MB to allow solving larger mazes.
                        maze_thread = new Thread(new ParameterizedThreadStart(SolveMaze), 20000000);
                        maze_thread.IsBackground = true;
                        maze_thread.Start(MazeInfo_struct);
                    }
                    catch (Exception message)
                    {
                        MessageBox.Show($"{message.Message}");
                    }

                }
               
            }
            else if (buttonSolve.Text == "Cancel")
            {
                stop = true;
                buttonSolve.Text = "Solve";
                listBoxData.Items.Add($"Thread has been canceled ");
            }

        }

        /******************************************************
         * Function: private void SolveMaze(object objdata)
         * Parameter: joined structure (including point and maze structure
         * return: void
         * Funtionality: called by the threading to solve the larger maze
         * recursively.
         * *****************************************************/
        private void SolveMaze(object objdata)
        {
           
            if (objdata is MazeInfo)
            {
                
                    MazeInfo maze_s = (MazeInfo)objdata;
                    Point p_ = maze_s.p_start;

                    MazeInfo maze_returned = ((Recursive_Method(p_, maze_s)));

                    //invoke to update the steps 
                    Invoke(new delVoidVoid(Update), maze_returned.steps, maze_returned.solved);
                

            }
        }

        /*********************************************************
         * Funtion: private void Update( int steps, bool solved)
         * Parameters:  int steps, bool solved
         * return : void
         * Funtionality: Invoked inside the thread funtion
         * Basically it updates the and shows the maze is solved or not
         *********************************************************/
        private void Update( int steps, bool solved)
        {
            if (stop)
            {
                listBoxData.Items.Add($"The thread Canceled. Total Threads {steps}");
            }
            else
            {
                if (solved == true)
                {
                    listBoxData.Items.Add($"Solved in {steps}");
                }
                else
                {
                    listBoxData.Items.Add($"Maze not solved");
                }
           }

        }

        //Lets you choose the dead color
        private void buttonDeadColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                MazeInfo_struct.Dead_color = colorDialog1.Color;
                buttonDeadColor.BackColor = colorDialog1.Color;
            }

        }

        //set the initial solve and dead color in the form
        private void Form1_Load(object sender, EventArgs e)
        {
            MazeInfo_struct.Sol_color = Color.Yellow;
            buttonSolveColor.BackColor = Color.Yellow;
            MazeInfo_struct.Dead_color = Color.Gray;
            buttonDeadColor.BackColor = Color.Gray;
        }
    }
}
