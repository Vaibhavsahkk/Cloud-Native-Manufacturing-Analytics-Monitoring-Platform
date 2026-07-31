import { Link, useLocation } from 'react-router-dom';

const Sidebar = () => {
  const location = useLocation();

  return (
    <aside className="app-sidebar">
      <div>
        <div className="sidebar-brand">
          <div className="brand-icon">M</div>
          <div>
            <div className="brand-name">MICRON MONITOR</div>
            <div className="brand-tag">Industrial IoT Core</div>
          </div>
        </div>

        <nav>
          <div style={{ fontSize: '0.72rem', textTransform: 'uppercase', color: 'var(--text-muted)', fontWeight: 700, letterSpacing: '0.08em', marginBottom: '12px', paddingLeft: '12px' }}>
            Telemetry Views
          </div>
          <Link
            to="/"
            className={`nav-item ${location.pathname === '/' ? 'active' : ''}`}
          >
            <span style={{ fontSize: '1.1rem' }}>📊</span>
            <span>Live Overview</span>
          </Link>
          <Link
            to="/metrics"
            className={`nav-item ${location.pathname === '/metrics' ? 'active' : ''}`}
          >
            <span style={{ fontSize: '1.1rem' }}>📈</span>
            <span>Historical Metrics</span>
          </Link>
        </nav>
      </div>

      <div className="glass-panel" style={{ padding: '16px', borderRadius: '10px' }}>
        <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', marginBottom: '8px' }}>
          <span style={{ fontSize: '0.78rem', color: 'var(--text-secondary)', fontWeight: 600 }}>Cluster Status</span>
          <span className="pulse-dot online"></span>
        </div>
        <div style={{ fontSize: '0.85rem', color: 'var(--text-primary)', fontWeight: 700 }}>
          Kubernetes Node-01
        </div>
        <div style={{ fontSize: '0.75rem', color: 'var(--accent-cyan)', fontFamily: 'var(--font-mono)', marginTop: '4px' }}>
          Latency: 14ms • SQL Sync
        </div>
      </div>
    </aside>
  );
};

export default Sidebar;
