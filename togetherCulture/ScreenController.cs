using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace togetherCulture
{
    public class ScreenController
    {
        private readonly Panel mainPanel; // The panel where UserControls are displayed.
        private readonly Dictionary<string, UserControl> screens; // Stores UserControls by name.

        public ScreenController(Panel panel)
        {
            mainPanel = panel ?? throw new ArgumentNullException(nameof(panel));
            screens = new Dictionary<string, UserControl>();
        }

        // Add a UserControl to the controller
        public void AddScreen(string name, UserControl control)
        {
            if (screens.ContainsKey(name))
                throw new ArgumentException($"A screen with the name '{name}' already exists.");

            screens[name] = control;
        }

        // Remove a UserControl from the controller
        public void RemoveScreen(string name)
        {
            if (!screens.ContainsKey(name))
                throw new KeyNotFoundException($"No screen with the name '{name}' exists.");

            if (mainPanel.Controls.Contains(screens[name]))
                mainPanel.Controls.Remove(screens[name]);

            screens.Remove(name);
        }

        // Show a specific UserControl
        public void ShowScreen(string name)
        {
            if (!screens.ContainsKey(name))
                throw new KeyNotFoundException($"No screen with the name '{name}' exists.");

            mainPanel.Controls.Clear();
            UserControl control = screens[name];
            control.Dock = DockStyle.Fill; // Make it fill the entire panel.
            mainPanel.Controls.Add(control);
        }
    }
}
