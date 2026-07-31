export interface Baseline {
  serviceName: string;
  from: string;
  to: string;
  averageCpuUsage: number;
  averageMemoryUsage: number;
  averageResponseTime: number;
  averageErrorCount: number;
}
