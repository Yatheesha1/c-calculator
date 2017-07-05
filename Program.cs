using System;
using Gtk;

namespace calc
{

	public class Calculator
	{

		static decimal result ;

		static decimal a ;
		static decimal b ;
		static string stringbutt="+";

		static string stringA ;
		static string stringB ;

		static int modify ;   // 0:a , 1:b      - which variable to modify
		static bool solved ;

		static void resultAction()
		{
			if (stringbutt == "/" && a == 0) 
			{
				Program.CalcScreen.Text = "Infinity";
				result = 0;
			}
			else
			    Program.CalcScreen.Text = result.ToString() ;
			solved = true ; 
			modify = 0; 
			stringA = ""; 
			stringB = ""; // These clear out values and warn other methods to do so as well. use in every method that defines result
		}


		static void process()
		{
			if (stringbutt == "+")
				result += a;
			else if (stringbutt == "-")
				result -= a;
			else if (stringbutt == "*")
				result *= a;
			else if (stringbutt == "/") 
			{   
				if (a != 0)
					result /= a;
					
			}
			else if (stringbutt == "=")
				result = a;
			else
				{}
			resultAction();
		}

		//Addition methods
		static void setbutt(object obj, EventArgs e)
		{
			
			Button btn = obj as Button;
			if (btn.Label == "C") 
			{
				result = 0;
				a = 0;
			}
			modify = 1 ; // if the addition button was clicked, we are no longer working with a so set modify to 1 indicating b
			process ();
			if(solved)
			{
				a = result ; 
			}
			solved = false;
			if (btn.Label != "C") 
			{
				stringbutt = btn.Label;
			}
		}
		public static EventHandler buttHandler = new EventHandler(setbutt);




		//clickButton definition and subscription preparation(instantiation as a delegate)
		static void clickNumButton(object obj, EventArgs e)
		{

			Button btn = obj as Button;
			if(stringA=="")
				Program.CalcScreen.Text="";
			if(modify == 0)
			{ 
				// we are working with the a variable. Append the value of the button clicked to the variable a

				stringA += btn.Label;


				if(String.Compare(stringA.Substring(stringA.Length - 1) , "." ) == 1 )
				{			
					a = decimal.Parse(stringA);
				}
				//Console.WriteLine("A: " + a + " string:" + stringA);
				if(!solved)
				{
					Program.CalcScreen.Text += btn.Label ;
				}
				else
				{
					Program.CalcScreen.Text = btn.Label ;
				} 
				solved = false ;

			}
			else
			{  
				// we are working with the b variable/ Append the value of the button clicked to the variable b
				
				stringB += btn.Label;

				if(String.Compare(stringB.Substring(stringB.Length-1) , "." ) == 1 )
				{
					b = decimal.Parse(stringB);
				}
				//Console.WriteLine("B: " + b + " string:" + stringB );
				if(!solved){
					Program.CalcScreen.Text += btn.Label ;
				}else{
					Program.CalcScreen.Text = btn.Label ;
				} 
				solved = false ;
			}


		}
		public static EventHandler clickNumber = new EventHandler(clickNumButton) ;

	};


	public class Program
	{
		// Declare the objects that will make up the window here so they are in the scope of any methods in Program, not just main.

		static Window MyWindow;	
		static Table  MyTable ;
		static Button One ;
		static Button Two ;
		static Button Three ;
		static Button Four ;
		static Button Five ;
		static Button Six ;
		static Button Seven ;
		static Button Eight ;
		static Button Nine ;
		static Button Cancel ;
		static Button Zero ;
		static Button DecPoint ;
		static Button Plus ;
		static Button Minus ;
		static Button Multi ;
		static Button Divided;
		public static Entry CalcScreen ;
		public static Button Equal ;

		static void Main()
		{
			Application.Init ();
			MyWindow = new Window ("Calculator");
			MyWindow.SetDefaultSize (500, 500);

			MyTable = new Table(6,4,true);
			MyWindow.Add(MyTable);

			One = new Button("1");
			MyTable.Attach(One, 0, 1, 3, 4);
			One.Clicked += Calculator.clickNumber ;

			Two = new Button("2");
			MyTable.Attach (Two, 1, 2, 3, 4);
			Two.Clicked += Calculator.clickNumber ;

			Three = new Button("3");
			MyTable.Attach(Three, 2, 3, 3, 4);
			Three.Clicked += Calculator.clickNumber ;

			Four = new Button("4");
			MyTable.Attach(Four, 0, 1, 2, 3);
		    Four.Clicked += Calculator.clickNumber ;

			Five = new Button("5");
			MyTable.Attach(Five, 1, 2, 2, 3);
			Five.Clicked += Calculator.clickNumber ;

			Six = new Button("6");
			MyTable.Attach(Six, 2, 3, 2, 3);
			Six.Clicked += Calculator.clickNumber ;

			Seven = new Button("7");
			MyTable.Attach(Seven, 0, 1, 1, 2);
			Seven.Clicked += Calculator.clickNumber ;

			Eight = new Button("8");
			MyTable.Attach(Eight, 1, 2, 1, 2);
			Eight.Clicked += Calculator.clickNumber ;

			Nine = new Button("9");
			MyTable.Attach(Nine, 2, 3, 1, 2);
			Nine.Clicked += Calculator.clickNumber ;

			Cancel = new Button("C");
			MyTable.Attach(Cancel, 0, 1, 4, 5);
			Cancel.Clicked += Calculator.buttHandler ;

			Zero = new Button("0");
			MyTable.Attach(Zero, 1, 2, 4, 5);
			Zero.Clicked += Calculator.clickNumber ;

			DecPoint = new Button(".");
			MyTable.Attach(DecPoint, 2, 3, 4, 5);
			DecPoint.Clicked += Calculator.clickNumber ;

			Equal = new Button("=");
			MyTable.Attach (Equal, 0, 4, 5, 6);
			Equal.Clicked += Calculator.buttHandler ;

			Plus = new Button("+");
			MyTable.Attach(Plus, 3, 4, 1, 2);
			Plus.Clicked += Calculator.buttHandler ;

			Minus = new Button("-");
			MyTable.Attach(Minus, 3, 4, 2, 3);
			Minus.Clicked += Calculator.buttHandler ;

			Multi = new Button("*");
			MyTable.Attach(Multi, 3, 4, 3, 4);
			Multi.Clicked += Calculator.buttHandler ;

			Divided = new Button("/");
			MyTable.Attach(Divided, 3, 4, 4, 5);
			Divided.Clicked += Calculator.buttHandler ;

			CalcScreen = new Entry();
			MyTable.Attach(CalcScreen, 0, 4, 0, 1);

			MyWindow.ShowAll ();
			Application.Run ();

		}

		static void window_close(object obj, DeleteEventArgs e)
		{// this needs these argument types so that it can subscribe to the delegate type DeleteEventHandler, which is defined in the GTK files. I do not know exactly why it uses these arguments.
			Application.Quit(); //Stop the application
			e.RetVal = true; // i don't really know what this line means
		}
	};

}
