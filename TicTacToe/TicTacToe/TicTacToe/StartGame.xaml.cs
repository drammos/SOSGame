using System.Collections.ObjectModel;

using TicTacToe.Box;
using TicTacToe.View;
namespace TicTacToe;

public partial class StartGame : ContentPage
{
	public ObservableCollection<TicTacToeBox> TicTacToeList { get; set; } = new ObservableCollection<TicTacToeBox>();
	
	public StartGame()
	{
		InitializeComponent();
		this.BindingContext = new TicTacToeGame();
	}
}