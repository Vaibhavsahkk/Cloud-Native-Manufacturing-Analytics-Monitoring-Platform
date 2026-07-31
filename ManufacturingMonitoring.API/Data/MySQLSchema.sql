-- ==============================================================================
-- Micron Manufacturing Monitoring Platform - MySQL Dual Database Schema
-- Description: MySQL 8.0 DDL & Index Definitions for Equipment Monitoring
-- DB Engine: MySQL 8.0 / MariaDB
-- ==============================================================================

CREATE DATABASE IF NOT EXISTS `mfg_monitoring_db` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE `mfg_monitoring_db`;

-- 1. Users Table
CREATE TABLE IF NOT EXISTS `Users` (
    `Id` INT AUTO_INCREMENT PRIMARY KEY,
    `Username` VARCHAR(50) NOT NULL UNIQUE,
    `Email` VARCHAR(100) NOT NULL UNIQUE,
    `Role` VARCHAR(20) NOT NULL DEFAULT 'User',
    `PasswordHash` VARCHAR(255) NOT NULL,
    `CreatedAt` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 2. Telemetry Metrics Table
CREATE TABLE IF NOT EXISTS `TelemetryMetrics` (
    `Id` INT AUTO_INCREMENT PRIMARY KEY,
    `NodeId` VARCHAR(50) NOT NULL,
    `NodeName` VARCHAR(100) NOT NULL,
    `CpuUsagePercent` DOUBLE NOT NULL,
    `MemoryUsageMb` DOUBLE NOT NULL,
    `TemperatureCelsius` DOUBLE NOT NULL,
    `Status` VARCHAR(20) NOT NULL DEFAULT 'Healthy',
    `Timestamp` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    INDEX `idx_timestamp_node` (`Timestamp` DESC, `NodeId`),
    INDEX `idx_status_cpu` (`Status`, `CpuUsagePercent` DESC)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 3. Alert Configurations Table
CREATE TABLE IF NOT EXISTS `AlertConfigs` (
    `Id` INT AUTO_INCREMENT PRIMARY KEY,
    `NodeId` VARCHAR(50) NOT NULL,
    `CpuThresholdPercent` DOUBLE NOT NULL DEFAULT 85.0,
    `TempThresholdCelsius` DOUBLE NOT NULL DEFAULT 75.0,
    `IsEmailAlertEnabled` TINYINT(1) NOT NULL DEFAULT 1,
    `CreatedAt` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
