import api from "./http";
import type { Metric } from "../models/Metric";
import type { Baseline } from "../models/Baseline";

export const getLatestMetric = async (
  serviceName: string
): Promise<Metric> => {
  const response = await api.get<Metric>(
    `/metrics/latest?serviceName=${serviceName}`
  );
  return response.data;
};

export const getMetricsHistory = async (
  serviceName: string,
  fromOrHours?: string | number,
  to?: string
): Promise<Metric[]> => {
  let fromStr = typeof fromOrHours === 'string' ? fromOrHours : new Date(Date.now() - (typeof fromOrHours === 'number' ? fromOrHours : 24) * 3600 * 1000).toISOString();
  let toStr = to || new Date().toISOString();
  
  const res = await api.get<Metric[]>(
    `/metrics/history?serviceName=${serviceName}&from=${fromStr}&to=${toStr}`
  );
  return res.data;
};

export const getHistoricalMetrics = getMetricsHistory;

export const getBaseline = async (
  serviceName: string,
  from: string,
  to: string
): Promise<Baseline> => {
  const res = await api.get<Baseline>(
    `/metrics/baseline?serviceName=${serviceName}&from=${from}&to=${to}`
  );
  return res.data;
};
