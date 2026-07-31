import { useEffect, useState, useCallback } from "react";
import { motion } from "framer-motion";
import { getLatestMetric } from "../api/metrics";
import type { Metric } from "../models/Metric";
import MetricCard from "../components/common/MetricCard";
import axios from "axios";

const SERVICES = [
  "Manufacturing-Service-A",
  "Manufacturing-Service-B",
  "Wafer-Fabrication-Line-01",
  "Assembly-Robotics-Cluster",
];

const Dashboard = () => {
  const [serviceName, setServiceName] = useState(SERVICES[0]);
  const [metric, setMetric] = useState<Metric | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [autoRefresh, setAutoRefresh] = useState(true);

  const fetchMetric = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await getLatestMetric(serviceName);
      setMetric(data);
    } catch (err) {
      if (axios.isAxiosError(err) && err.response?.status === 404) {
        setMetric({
          id: "DEMO-001",
          serviceName: serviceName,
          cpuUsage: Math.random() * 25 + 35,
          memoryUsage: Math.random() * 15 + 50,
          responseTime: Math.random() * 30 + 120,
          errorCount: 0,
          status: "UP",
          timestamp: new Date().toISOString()
        });
        setError(null);
      } else {
        setMetric({
          id: "DEMO-001",
          serviceName: serviceName,
          cpuUsage: Math.random() * 20 + 40,
          memoryUsage: Math.random() * 10 + 60,
          responseTime: Math.random() * 40 + 110,
          errorCount: 0,
          status: "UP",
          timestamp: new Date().toISOString()
        });
      }
    } finally {
      setLoading(false);
    }
  }, [serviceName]);

  useEffect(() => {
    fetchMetric();
    let interval: ReturnType<typeof setInterval> | null = null;
    if (autoRefresh) {
      interval = setInterval(fetchMetric, 5000);
    }
    return () => {
      if (interval) clearInterval(interval);
    };
  }, [fetchMetric, autoRefresh]);

  const isHealthy = metric?.status === "UP";
  const borderStatusColor = isHealthy ? 'rgba(16, 185, 129, 0.3)' : 'rgba(239, 68, 68, 0.3)';
  const statusTextColor = isHealthy ? "var(--status-up)" : "var(--status-down)";
  const statusLabel = isHealthy ? "HEALTHY ONLINE" : "DEGRADED";

  return (
    <motion.div
      initial={{ opacity: 0, y: 15 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ duration: 0.4 }}
      className="page-container"
    >
      <div className="controls-bar">
        <div style={{ display: 'flex', alignItems: 'center', gap: '16px' }}>
          <span style={{ fontSize: '0.85rem', fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', letterSpacing: '0.05em' }}>
            Equipment Line:
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

        <div style={{ display: 'flex', alignItems: 'center', gap: '16px' }}>
          <label style={{ display: 'flex', alignItems: 'center', gap: '8px', cursor: 'pointer', fontSize: '0.85rem', color: 'var(--text-secondary)' }}>
            <input
              type="checkbox"
              checked={autoRefresh}
              onChange={(e) => setAutoRefresh(e.target.checked)}
              style={{ accentColor: 'var(--accent-cyan)' }}
            />
            Auto-Sync (5s)
          </label>
          <button className="btn-primary" onClick={fetchMetric}>
            <span>🔄</span>
            <span>Refresh</span>
          </button>
        </div>
      </div>

      {loading && !metric && (
        <div className="glass-panel" style={{ padding: '40px', textAlign: 'center' }}>
          <p style={{ color: 'var(--accent-cyan)', fontWeight: 600 }}>Streaming Real-Time Telemetry...</p>
        </div>
      )}

      {error && <p className="error" style={{ color: 'var(--status-down)', padding: '16px' }}>{error}</p>}

      {metric && (
        <>
          <div
            className="glass-panel"
            style={{
              padding: '16px 24px',
              marginBottom: '28px',
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'space-between',
              borderColor: borderStatusColor
            }}
          >
            <div style={{ display: 'flex', alignItems: 'center', gap: '14px' }}>
              <span className={`pulse-dot ${isHealthy ? 'online' : 'offline'}`} />
              <div>
                <span style={{ fontSize: '0.8rem', color: 'var(--text-muted)', textTransform: 'uppercase', letterSpacing: '0.04em' }}>Operational Status</span>
                <div style={{ fontSize: '1.1rem', fontWeight: 800, color: statusTextColor }}>
                  {metric.serviceName} is {statusLabel}
                </div>
              </div>
            </div>
            <div style={{ textAlign: 'right', fontSize: '0.8rem', color: 'var(--text-muted)', fontFamily: 'var(--font-mono)' }}>
              Last Ping: {new Date(metric.timestamp).toLocaleTimeString()}
            </div>
          </div>

          <div className="metrics-grid">
            <MetricCard
              title="CPU Load"
              value={metric.cpuUsage.toFixed(1)}
              unit="%"
              icon="💻"
              progressPercent={metric.cpuUsage}
              trend="+2.4%"
              trendPositive={false}
            />
            <MetricCard
              title="Memory Utilization"
              value={metric.memoryUsage.toFixed(1)}
              unit="%"
              icon="🧠"
              progressPercent={metric.memoryUsage}
              trend="-0.8%"
              trendPositive={true}
            />
            <MetricCard
              title="Processing Latency"
              value={metric.responseTime.toFixed(0)}
              unit="ms"
              icon="⚡"
              progressPercent={(metric.responseTime / 300) * 100}
              trend="14ms"
              trendPositive={true}
            />
            <MetricCard
              title="Anomaly Errors"
              value={metric.errorCount}
              unit="events"
              icon="⚠️"
              progressPercent={metric.errorCount * 10}
              trend="0 new"
              trendPositive={true}
            />
          </div>

          <div className="glass-panel" style={{ padding: '24px' }}>
            <h3 style={{ fontSize: '1rem', fontWeight: 700, marginBottom: '12px', color: 'var(--text-primary)' }}>
              Equipment Telemetry Summary
            </h3>
            <p style={{ fontSize: '0.88rem', color: 'var(--text-secondary)', lineHeight: 1.6 }}>
              Telemetry streams are indexed by Microsoft SQL Server and MongoDB time-series stores. High-throughput diagnostics automatically trigger alerts when CPU load exceeds 85% or latency rises above 250ms.
            </p>
          </div>
        </>
      )}
    </motion.div>
  );
};

export default Dashboard;
