#!/usr/bin/env perl
use strict;
use warnings;
use Time::Piece;
use JSON;

# ==============================================================================
# Micron Manufacturing Monitoring Platform - Perl Telemetry Generator Script
# Description: Generates realistic time-series equipment telemetry metrics (CPU %,
#              Memory, Temperature, IOPS) across 50 simulated semiconductor fab nodes.
# Tech Stack: Perl 5, JSON, DBI/SQL Compatible Output
# ==============================================================================

print "=================================================================\n";
print " Micron Semiconductor Fab Telemetry Seeder (Perl 5 Diagnostic Engine)\n";
print "=================================================================\n\n";

my $node_count = 50;
my @metrics = ();

for (my $i = 1; $i <= $node_count; $i++) {
    my $node_id = sprintf("MFG-NODE-%03d", $i);
    my $node_name = sprintf("Fab Machine Chamber #%02d", $i);
    my $cpu = sprintf("%.1f", 20.0 + rand(70.0));
    my $mem = sprintf("%.0f", 2048 + rand(14336));
    my $temp = sprintf("%.1f", 35.0 + rand(45.0));
    my $status = ($cpu > 85.0) ? "Critical" : ($cpu > 70.0 ? "Warning" : "Healthy");
    my $timestamp = gmtime()->datetime . "Z";

    push @metrics, {
        id => $i,
        nodeId => $node_id,
        nodeName => $node_name,
        cpuUsagePercent => $cpu + 0,
        memoryUsageMb => $mem + 0,
        temperatureCelsius => $temp + 0,
        status => $status,
        timestamp => $timestamp
    };

    print "[PERL SEEDER] Generated Node: $node_id ($node_name) -> CPU: $cpu%, Temp: ${temp}C [$status]\n";
}

my $json_output = encode_json(\@metrics);
my $output_file = "scripts/simulated_telemetry_seed.json";

open(my $fh, '>', $output_file) or die "Could not open file '$output_file' $!";
print $fh $json_output;
close($fh);

print "\n=================================================================\n";
print " SUCCESS: Generated $node_count node metrics to '$output_file'\n";
print "=================================================================\n";
