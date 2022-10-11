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
            byte count = 0;
            CalculatePids(ref count);
            if (count == 0) return;
            CreateWndRenderArray(count);
        }

        private void CalculatePids(ref byte count)
        {
            foreach (var item in map_pids)
            {
                if (item.Value == true) count++;
            }
        }

        private void CreateWndRenderArray(byte count)
        {
            _renderers = new RendererConrainerForm[count];
            ushort id_renderer = 0;
            foreach (var item in map_pids)
            {
                if (item.Value)
                {
                    _renderers[id_renderer] = new RendererConrainerForm(item.Key);
                    _renderers[id_renderer].Show();
                    id_renderer++;
                }
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