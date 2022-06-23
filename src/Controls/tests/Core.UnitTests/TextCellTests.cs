using System;
using Xunit;


namespace Microsoft.Maui.Controls.Core.UnitTests
{
	
	public class TextCellTests : BaseTestFixtureXUnit
	{
		[Fact]
		public void TestTapped()
		{
			var cell = new TextCell();
			bool tapped = false;
			cell.Tapped += (sender, args) => tapped = true;

			cell.OnTapped();
			Assert.True(tapped);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void TappedHonorsCanExecute(bool canExecute)
		{
			bool executed = false;

			var cmd = new Command(() => executed = true, () => canExecute);
			var cell = new TextCell { Command = cmd };
			cell.OnTapped();

			Assert.That(executed, Is.EqualTo(canExecute));
		}

		[Fact]
		public void TestCommand()
		{
			bool executed = false;

			var cmd = new Command(() => executed = true);
			var cell = new TextCell();
			cell.Command = cmd;
			cell.OnTapped();

			Assert.True(executed, "Command was not executed");
		}

		[Fact]
		public void TestCommandParameter()
		{
			bool executed = false;

			object obj = new object();
			var cmd = new Command(p =>
			{
				Assert.AreSame(obj, p);
				executed = true;
			});

			var cell = new TextCell
			{
				Command = cmd,
				CommandParameter = obj
			};

			cell.OnTapped();

			Assert.True(executed, "Command was not executed");
		}

		[Fact]
		public void TestCommandCanExecute()
		{
			bool tested = false;

			var cmd = new Command(() => { },
				canExecute: () =>
				{
					tested = true;
					return true;
				});

			new TextCell { Command = cmd };
			Assert.True(tested, "Command.CanExecute was not called");
		}

		[Fact]
		public void TestCommandCanExecuteDisables()
		{
			var cmd = new Command(() => { }, () => false);
			var cell = new TextCell { Command = cmd };
			Assert.False(cell.IsEnabled, "Cell was not disabled");
		}

		[Fact]
		public void TestCommandCanExecuteChanged()
		{
			bool first = true;
			var cmd = new Command(() => { }, () =>
			{
				if (first)
				{
					first = false;
					return false;
				}
				else
				{
					return true;
				}
			});

			var cell = new TextCell { Command = cmd };
			Assert.False(cell.IsEnabled, "Cell was not disabled");

			cmd.ChangeCanExecute();

			Assert.True(cell.IsEnabled, "Cell was not reenabled");
		}

		[Fact]
		public void Create()
		{
			var template = new DataTemplate(typeof(TextCell));
			var content = template.CreateContent();

			Assert.NotNull(content);
			Assert.That(content, Is.InstanceOf<TextCell>());
		}

		[Fact]
		public void Detail()
		{
			var template = new DataTemplate(typeof(TextCell));
			template.SetValue(TextCell.DetailProperty, "detail");

			TextCell cell = (TextCell)template.CreateContent();
			Assert.That(cell.Detail, Is.EqualTo("detail"));
		}

		[Fact]
		public void Text()
		{
			var template = new DataTemplate(typeof(TextCell));
			template.SetValue(TextCell.TextProperty, "text");

			TextCell cell = (TextCell)template.CreateContent();
			Assert.That(cell.Text, Is.EqualTo("text"));
		}
	}
}
