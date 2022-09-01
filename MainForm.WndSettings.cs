namespace VideoGraphSample
{
    public partial class MainForm
    {
        private void CreateWndRender()
        {
            _renderers = new RendererConrainerForm[5];
            byte id = 85;
            for (byte ind = 0; ind < _renderers.Length; ind++)
            {
                _renderers[ind] = new RendererConrainerForm("x00" + id.ToString());
                _renderers[ind].Show();
                id++;
            }
        }

        private void CloseWndRender()
        {
            foreach (var render in _renderers)
            {
                render.Close();
            }
            _renderers = null;
            
        }
    }
}