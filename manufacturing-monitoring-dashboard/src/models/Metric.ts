export interface Metric {
  id?: string;
  serviceName: string;
  cpuUsage: number;
  memoryUsage: number;
  responseTime: number;
  errorCount: number;
  status: "UP" | "DOWN";
  timestamp: string;
}
