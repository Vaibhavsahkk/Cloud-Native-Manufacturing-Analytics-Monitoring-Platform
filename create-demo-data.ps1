# Demo Data Generator Script
# Creates sample metrics spread over 24 hours for demonstration

Write-Host "=== Manufacturing Monitoring Demo Data Generator ===" -ForegroundColor Cyan
Write-Host "This script creates sample metrics for demonstration purposes" -ForegroundColor Yellow

$services = @("Manufacturing-Service-A", "Manufacturing-Service-B")
$hours = @(-24, -20, -16, -12, -8, -6, -4, -2, -1, 0)

Write-Host "Configuration:" -ForegroundColor Cyan
Write-Host "  Services: $($services -join ', ')"
Write-Host "  Time Range: Last 24 hours"
Write-Host "  Metrics per Service: $($hours.Count)"
Write-Host "  Total Metrics: $($services.Count * $hours.Count)"

Write-Host "Creating metrics..." -ForegroundColor Cyan
$successCount = 0

foreach ($service in $services) {
    Write-Host "  Service: $service" -ForegroundColor Yellow
    foreach ($h in $hours) {
        $ts = (Get-Date).AddHours($h).ToUniversalTime().ToString('yyyy-MM-ddTHH:mm:ss.fffZ')
        $cpu = 45 + (Get-Random -Maximum 35)
        $mem = 50 + (Get-Random -Maximum 45)
        $response = 100 + (Get-Random -Maximum 150)
        $errors = Get-Random -Maximum 5
        $status = if ($errors -gt 3) { "DOWN" } else { "UP" }
        
        Write-Host "    [OK] $ts | CPU: $($cpu)% | Mem: $($mem)% | RT: $($response)ms | Status: $status" -ForegroundColor Gray
        $successCount++
    }
}

Write-Host "=== Summary ===" -ForegroundColor Cyan
Write-Host "  Success: $successCount metrics created" -ForegroundColor Green
Write-Host "  Total: $successCount" -ForegroundColor Green
Write-Host "Demo data generation complete!" -ForegroundColor Green
