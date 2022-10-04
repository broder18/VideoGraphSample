using System.Collections.Generic;

namespace VideoGraphSample
{
    public partial class MainForm
    {
        private void CreateMap()
        {
            map_pids = new Dictionary<ushort, bool>()
            {
                {133, false },
                {134, false },
                {135, false },
                {136, false },
                {137, false }
            };
        }

        private void CreateWndRender()
        {             
            if (map_pids.Count == 0) return;
            _renderers = new RendererConrainerForm[map_pids.Count];

            ushort id_renderer = 0;
            foreach(var item in map_pids)
            {
                _renderers[id_renderer] = new RendererConrainerForm(item.Key);
                _renderers[id_renderer].Show();
                id_renderer++;
            }
        }

        private void CloseWndRender()
        {
            if (_renderers == null) return;
            foreach (var render in _renderers)
            {
                render.Close();
            }
            _renderers = null;
            
        }
    }
}