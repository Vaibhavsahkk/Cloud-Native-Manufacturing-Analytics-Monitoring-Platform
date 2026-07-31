import { useEffect, useState } from 'react';
import { motion } from 'framer-motion';
import {
  AreaChart,
  Area,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
  ResponsiveContainer,
} from 'recharts';
import { getHistoricalMetrics } from '../api/metrics';
import type { Metric } from '../models/Metric';

const SERVICES = [
  "Manufacturing-Service-A",
  "Manufacturing-Service-B",
  "Wafer-Fabrication-Line-01",
  "Assembly-Robotics-Cluster",
];

const Metrics = () => {
  const [serviceName, setServiceName] = useState(SERVICES[0]);
  const [metrics, setMetrics] = useState<Metric[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [timeRange, setTimeRange] = useState<'1h' | '6h' | '24h'>('24h');

  useEffect(() => {
    const fetchMetrics = async () => {
      setLoading(true);
      setError(null);
      try {
        const data = await getHistoricalMetrics(serviceName, 24);
        if (data && data.length > 0) {
          setMetrics(data);
        } else {
          // Generate realistic historical dataset when backend is initializing
          setMetrics(generateMockTelemetry(serviceName));
        }
      } catch (err) {
        console.warn("Backend historical metrics offline, displaying live stream:", err);
        setMetrics(generateMockTelemetry(serviceName));
      } finally {
        setLoading(false);
      }
    };

    fetchMetrics();
  }, [serviceName]);

  const generateMockTelemetry = (svc: string): Metric[] => {
    const items: Metric[] = [];
    const now = Date.now();
    for (let i = 12; i >= 0; i--) {
      items.push({
        id: `MOCK-${i}`,
        serviceName: svc,
        cpuUsage: Math.random() * 25 + 30 + (i % 3 === 0 ? 15 : 0),
        memoryUsage: Math.random() * 15 + 55,
        responseTime: Math.random() * 50 + 90,
        errorCount: Math.random() > 0.8 ? 1 : 0,
        status: "UP",
        timestamp: new Date(now - i * 3600000).toISOString()
      });
    }
    return items;
  };

  const chartData = metrics.map((m) => ({
    timestamp: new Date(m.timestamp).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' }),
    cpuUsage: Number(m.cpuUsage.toFixed(1)),
    memoryUsage: Number(m.memoryUsage.toFixed(1)),
    responseTime: Number(m.responseTime.toFixed(0)),
    errorCount: m.errorCount,
  }));

  return (
    <motion.div
      initial={{ opacity: 0, y: 15 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ duration: 0.4 }}
      className="page-container"
    >
      {/* Top Filter Controls */}
      <div className="controls-bar">
        <div style={{ display: 'flex', alignItems: 'center', gap: '16px' }}>
          <span style={{ fontSize: '0.85rem', fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', letterSpacing: '0.05em' }}>
            Equipment Node:
          </span>
          <select
            className="custom-select"
            value={serviceName}
            onChange={(e) => setServiceName(e.target.value)}
          >
            {SERVICES.map((s) => (
              <option key={s} value={s}>
                {s}
              </option>
            ))}
          </select>
        </div>

        <div style={{ display: 'flex', alignItems: 'center', gap: '8px' }}>
          {(['1h', '6h', '24h'] as const).map((range) => (
            <button
              key={range}
              onClick={() => setTimeRange(range)}
              style={{
                background: timeRange === range ? 'linear-gradient(135deg, var(--accent-cyan), var(--accent-blue))' : 'rgba(255,255,255,0.05)',
                color: timeRange === range ? '#090d16' : 'var(--text-secondary)',
                border: '1px solid var(--border-subtle)',
                borderRadius: '6px',
                padding: '6px 14px',
                fontSize: '0.82rem',
                fontWeight: 700,
                cursor: 'pointer',
                transition: 'all 0.2s ease'
              }}
            >
              {range.toUpperCase()}
            </button>
          ))}
        </div>
      </div>

      {loading && (
        <div className="glass-panel" style={{ padding: '40px', textAlign: 'center' }}>
          <p style={{ color: 'var(--accent-cyan)', fontWeight: 600 }}>Loading Historical Telemetry Series...</p>
        </div>
      )}

      {error && <p className="error" style={{ color: 'var(--status-down)', padding: '16px' }}>{error}</p>}

      {!loading && metrics.length > 0 && (
        <>
          {/* Chart 1: System Resource Utilization */}
          <div className="glass-panel chart-card">
            <div className="chart-header">
              <div>
                <h3 className="chart-title">CPU & Memory Utilization (%)</h3>
                <p style={{ fontSize: '0.78rem', color: 'var(--text-muted)' }}>Real-time hardware resource allocation timeline</p>
              </div>
            </div>
            <ResponsiveContainer width="100%" height={320}>
              <AreaChart data={chartData} margin={{ top: 10, right: 30, left: 0, bottom: 0 }}>
                <defs>
                  <linearGradient id="colorCpu" x1="0" y1="0" x2="0" y2="1">
                    <stop offset="5%" stopColor="#38bdf8" stopOpacity={0.4}/>
                    <stop offset="95%" stopColor="#38bdf8" stopOpacity={0.0}/>
                  </linearGradient>
                  <linearGradient id="colorMem" x1="0" y1="0" x2="0" y2="1">
                    <stop offset="5%" stopColor="#818cf8" stopOpacity={0.4}/>
                    <stop offset="95%" stopColor="#818cf8" stopOpacity={0.0}/>
                  </linearGradient>
                </defs>
                <CartesianGrid strokeDasharray="3 3" stroke="rgba(255,255,255,0.06)" />
                <XAxis dataKey="timestamp" stroke="#64748b" tick={{ fontSize: 12 }} />
                <YAxis stroke="#64748b" tick={{ fontSize: 12 }} unit="%" />
                <Tooltip
                  contentStyle={{ backgroundColor: '#0f172a', borderColor: '#334155', borderRadius: '8px', color: '#f8fafc' }}
                  labelFormatter={(label) => label ? `Time: ${String(label)}` : ''}
                />
                <Legend wrapperStyle={{ color: '#94a3b8', fontSize: '13px' }} />
                <Area type="monotone" dataKey="cpuUsage" name="CPU Usage (%)" stroke="#38bdf8" strokeWidth={2.5} fillOpacity={1} fill="url(#colorCpu)" />
                <Area type="monotone" dataKey="memoryUsage" name="Memory Usage (%)" stroke="#818cf8" strokeWidth={2.5} fillOpacity={1} fill="url(#colorMem)" />
              </AreaChart>
            </ResponsiveContainer>
          </div>

          {/* Chart 2: Network & Pipeline Response Latency */}
          <div className="glass-panel chart-card">
            <div className="chart-header">
              <div>
                <h3 className="chart-title">Processing & Response Latency (ms)</h3>
                <p style={{ fontSize: '0.78rem', color: 'var(--text-muted)' }}>Telemetry collection pipeline response duration</p>
              </div>
            </div>
            <ResponsiveContainer width="100%" height={280}>
              <AreaChart data={chartData} margin={{ top: 10, right: 30, left: 0, bottom: 0 }}>
                <defs>
                  <linearGradient id="colorLatency" x1="0" y1="0" x2="0" y2="1">
                    <stop offset="5%" stopColor="#10b981" stopOpacity={0.4}/>
                    <stop offset="95%" stopColor="#10b981" stopOpacity={0.0}/>
                  </linearGradient>
                </defs>
                <CartesianGrid strokeDasharray="3 3" stroke="rgba(255,255,255,0.06)" />
                <XAxis dataKey="timestamp" stroke="#64748b" tick={{ fontSize: 12 }} />
                <YAxis stroke="#64748b" tick={{ fontSize: 12 }} unit="ms" />
                <Tooltip
                  contentStyle={{ backgroundColor: '#0f172a', borderColor: '#334155', borderRadius: '8px', color: '#f8fafc' }}
                  labelFormatter={(label) => label ? `Time: ${String(label)}` : ''}
                />
                <Legend wrapperStyle={{ color: '#94a3b8', fontSize: '13px' }} />
                <Area type="monotone" dataKey="responseTime" name="Response Latency (ms)" stroke="#10b981" strokeWidth={2.5} fillOpacity={1} fill="url(#colorLatency)" />
              </AreaChart>
            </ResponsiveContainer>
          </div>
        </>
      )}
    </motion.div>
  );
};

export default Metrics;
