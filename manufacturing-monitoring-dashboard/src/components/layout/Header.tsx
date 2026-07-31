import { useState, useEffect } from 'react';

const Header = () => {
  const [time, setTime] = useState<string>('');

  useEffect(() => {
    const updateClock = () => {
      const now = new Date();
      setTime(now.toLocaleTimeString('en-US', { hour12: false, hour: '2-digit', minute: '2-digit', second: '2-digit' }));
    };
    updateClock();
    const interval = setInterval(updateClock, 1000);
    return () => clearInterval(interval);
  }, []);

  return (
    <header className="app-header">
      <div className="header-title-group">
        <div>
          <h1 className="header-title">Cloud-Native Manufacturing Intelligence</h1>
          <p className="header-subtitle">Real-Time Telemetry & Diagnostic Analytics Platform</p>
        </div>
      </div>

      <div style={{ display: 'flex', alignItems: 'center', gap: '20px' }}>
        <div style={{ textAlign: 'right' }}>
          <div style={{ fontSize: '0.72rem', color: 'var(--text-muted)', fontWeight: 600, textTransform: 'uppercase', letterSpacing: '0.05em' }}>System Clock</div>
          <div style={{ fontSize: '0.95rem', fontFamily: 'var(--font-mono)', fontWeight: 700, color: 'var(--accent-cyan)' }}>
            {time || '00:00:00'} UTC
          </div>
        </div>
        <div className="header-badge">
          v2.4.0 Production
        </div>
      </div>
    </header>
  );
};

export default Header;
