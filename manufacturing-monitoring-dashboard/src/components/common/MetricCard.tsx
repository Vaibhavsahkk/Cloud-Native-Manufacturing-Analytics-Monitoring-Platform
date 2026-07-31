import React from 'react';

interface Props {
  title: string;
  value: string | number;
  unit?: string;
  icon?: string;
  progressPercent?: number;
  trend?: string;
  trendPositive?: boolean;
}

const MetricCard: React.FC<Props> = ({
  title,
  value,
  unit,
  icon = '⚡',
  progressPercent,
  trend,
  trendPositive = true,
}) => {
  return (
    <div className="glass-panel metric-card-wrapper">
      <div className="metric-card-header">
        <span className="metric-title">{title}</span>
        <div className="metric-icon-badge">{icon}</div>
      </div>

      <div className="metric-value-container">
        <span className="metric-value">{value}</span>
        {unit && <span className="metric-unit">{unit}</span>}
      </div>

      {trend && (
        <div style={{ marginTop: '10px', fontSize: '0.78rem', display: 'flex', alignItems: 'center', gap: '6px' }}>
          <span style={{ color: trendPositive ? 'var(--status-up)' : 'var(--status-down)', fontWeight: 700 }}>
            {trendPositive ? '↑' : '↓'} {trend}
          </span>
          <span style={{ color: 'var(--text-muted)' }}>vs 1h average</span>
        </div>
      )}

      {progressPercent !== undefined && (
        <div className="metric-progress-bg">
          <div
            className="metric-progress-fill"
            style={{ width: `${Math.min(100, Math.max(0, progressPercent))}%` }}
          />
        </div>
      )}
    </div>
  );
};

export default MetricCard;
