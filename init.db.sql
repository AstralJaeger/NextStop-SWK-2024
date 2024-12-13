-- Check if the database objects exist before creating them
DO $$
    BEGIN
        
        -- Create enumeration for holiday type
       -- CREATE TYPE holiday_type AS ENUM ('BankHoliday', 'SchoolVacation', 'NationalHoliday', 'ReligiousHoliday', 'Other');
        
        
        -- Create holiday table if it does not exist
        CREATE TABLE IF NOT EXISTS holiday (
            id SERIAL PRIMARY KEY,           -- Unique identifier for each holiday
            name VARCHAR(255) NOT NULL,      -- Name of the holiday
            start_date DATE NOT NULL,        -- Start date and time of the holiday
            end_date DATE NOT NULL,          -- End date and time of the holiday
            typ holiday_type NOT NULL        -- Type of holiday (e.g., BankHoliday, SchoolVacation)
        );

        -- Create route table if it does not exist
        CREATE TABLE IF NOT EXISTS route (
            id SERIAL PRIMARY KEY,
            name VARCHAR(255) NOT NULL,
            valid_from DATE NOT NULL,
            valid_to DATE NOT NULL,
            valid_on INTEGER NOT NULL
        );

        -- Create stoppoint table if it does not exist
        CREATE TABLE IF NOT EXISTS stoppoint (
            id SERIAL PRIMARY KEY,
            name VARCHAR(255) NOT NULL,
            short_name VARCHAR(100) NOT NULL,
            latitude FLOAT NOT NULL,
            longitude FLOAT NOT NULL
        );

        -- Create routestoppoint table if it does not exist
        CREATE TABLE IF NOT EXISTS routestoppoint (
            id SERIAL PRIMARY KEY,
            route_id INTEGER REFERENCES route(id),
            stop_point_id INTEGER REFERENCES stoppoint(id),
            arrival_time TIMESTAMP NOT NULL,
            departure_time TIMESTAMP NOT NULL,
            order_number INTEGER NOT NULL
        );

        -- Create trip table if it does not exist
        CREATE TABLE IF NOT EXISTS trip (
            id SERIAL PRIMARY KEY,
            route_id INTEGER REFERENCES route(id),
            vehicle_id INTEGER NOT NULL
        );

        -- Create tripcheckin table if it does not exist
        CREATE TABLE IF NOT EXISTS tripcheckin (
            id SERIAL PRIMARY KEY,
            trip_id INTEGER REFERENCES trip(id),
            stop_point_id INTEGER REFERENCES stoppoint(id),
            checkin_time TIMESTAMP NOT NULL
        );

-----------------------------------------------------------------------------------------------------------------
        -- Dummy Data

        -- Insert sample data into holiday table
        INSERT INTO holiday (name, start_date, end_date, typ) VALUES
            ('Maria Empfängnis', '2024-12-08', '2024-12-08', 'NationalHoliday'),
            ('Weihnachtsferien', '2024-12-23', '2025-01-06', 'SchoolVacation'),
            ('Weihnachten', '2024-12-25', '2024-12-25', 'NationalHoliday'),
            ('Stefanitag', '2024-12-26', '2024-12-26', 'NationalHoliday'),
            ('Neujahr', '2025-01-01', '2025-01-01', 'NationalHoliday'),
            ('Heilige Drei Könige', '2025-01-06', '2025-01-06', 'NationalHoliday'),
            ('Ostersonntag', '2025-04-20', '2025-04-20', 'NationalHoliday'),
            ('Ostermontag', '2025-04-21', '2025-04-21', 'NationalHoliday'),
            ('Staatsfeiertag', '2025-05-01', '2025-05-01', 'NationalHoliday'),
            ('Christi Himmelfahrt', '2025-05-29', '2025-05-29', 'NationalHoliday'),
            ('Pfingstsonntag', '2025-06-08', '2025-06-08', 'NationalHoliday'),
            ('Sommerferien', '2025-07-05', '2025-08-07', 'SchoolVacation')
        ON CONFLICT DO NOTHING;

-- Insert sample data into route table
        INSERT INTO route (name, valid_from, valid_to, valid_on) VALUES
            ('Linie 1-1', '2024-01-01', '2024-12-31', 62),
            ('Linie 1-2', '2024-01-01', '2024-12-31', 62),
            
            ('Linie 2-1', '2024-01-01', '2024-12-31', 62),
            ('Linie 2-2', '2024-01-01', '2024-12-31', 62),
            
            ('Linie 3-1', '2024-01-01', '2024-12-31', 62),
            ('Linie 3-2', '2024-01-01', '2024-12-31', 62),
            
            ('Linie 4-1', '2024-01-01', '2024-12-31', 62),
            ('Linie 4-2', '2024-01-01', '2024-12-31', 62)
            
            -- ('Green', '2024-01-01', '2025-12-31', 62),  -- 62 (0b0111110) für Montag bis Freitag
            -- ('Blue', '2024-03-01', '2025-09-30', 85),   -- 85 (0b1010101) für Mo, Mi, Fr, So
            -- ('Red', '2024-08-08', '2025-11-05', 85)     -- 85 (0b1010101) für Mo, Mi, Fr, So
        ON CONFLICT DO NOTHING;

-- Insert sample data into stoppoint table
        INSERT INTO stoppoint (name, short_name, latitude, longitude) VALUES
            -- Stop Points for Line 1
            ('JKU | Universität', 'JKU', 48.3339902, 14.3204371),
            ('Schumpeterstraße', 'SPS', 48.3317917, 14.318887),
            ('Dornach', 'Don', 48.3318618, 14.3146586),
            ('Glaserstraße', 'GaS', 48.3325591, 14.3094093),
            ('St. Magdalena', 'StM', 48.3337188,14.3019944),
            ('Ferdinand-Markl-Straße', 'FMS', 48.3337695,14.2970822),
            ('Gründberg', 'GueB', 48.3317043,14.2923596),
            ('Harbachsiedlung', 'HBS', 48.3302745,14.2899509),
            ('Harbach', 'HaB', 48.3269954,14.287179),
            ('Ontlstraße', 'OtS', 48.3221512,14.2867603),
            ('Linke Brückenstraße', 'LBS', 48.3196493,14.2903772),
            ('Ferihumerstraße', 'FHS', 48.3161919,14.2907568),
            ('Peuerbachstraße', 'PBS', 48.3151876,14.2897847),
            ('Wildbergstraße', 'WBS', 48.3130008,14.2872969),
            ('Rudolfstraße', 'RuD', 48.3110363,14.283512),
            ('Hauptplatz', 'HaP', 48.3061676,14.2869078),
            ('Taubenmarkt', 'TaM', 48.3037221,14.2888917),
            ('Mozartkreuzung', 'MoK', 48.3007778,14.2913964),
            ('Bürgerstraße', 'BueS', 48.2984966,14.2919969),
            ('Goethekreuzung', 'GoeK', 48.2957225,14.2926083),
            ('Hauptbahnhof', 'HBF', 48.2906122,14.2926491),
            ('Unionkreuzung', 'UiK', 48.2901944,14.2981889),
            ('Herz-Jesu-Kirche', 'HJK', 48.2872637,14.3010894),
            ('Bulgariplatz', 'BuP', 48.2851041,14.3028567),
            ('WIFI/LINZ AG', 'LAG', 48.2807653,14.3096478),
            ('Turmstraße', 'TuS', 48.2755235,14.3146728),
            ('Neue Welt', 'NWe', 48.2734024,14.314782),
            ('Scharlinz', 'Sal', 48.2686571,14.317025),
            ('Wahringerstraße', 'WaS', 48.2652301,14.3189548),
            ('Wimmerstraße', 'WiS', 48.2615672,14.3206328),
            ('Remise Kleinmünchen', 'RKl', 48.2583294,14.3223298),
            ('Simonystraße', 'SiS', 48.2553327,14.3236033),
            ('Dürerstraße', 'DueS', 48.2537792,14.3189698),
            ('Dauphinestraße', 'DauS', 48.2527207,14.3149927),
            ('Rädlerweg', 'RaeW', 48.2509847,14.3121717),
            ('Auwiesen', 'AuW', 48.2483885,14.3088239),

            -- Stoppoints for Line 2 (which have not been added yet)
            ('Saporoshjestraße', 'SaS', 48.2525703,14.3238078),
            ('Ebelsberg', 'EeB', 48.2457603,14.327724),
            ('Wambacher Straße', 'WBS', 48.2410198,14.32891),
            ('Edmund-Aigner-Straße', 'EAS', 48.2405847,14.3316143),
            ('Hartheimerstraße', 'HHS', 48.2406575,14.3346287),
            ('Ennsfeld', 'Efe', 48.2426225,14.3381264),
            ('Hillerstraße', 'HiS', 48.2483002,14.3408031),
            ('Ebelsberg Bahnhof', 'EBB', 48.2521689,14.3485517),
            ('Neufelderstraße', 'NFS', 48.2560085,14.35748),
            ('solarCity-Zentrum', 'SCZ', 48.2574891,14.3604311),
            ('solarCity', 'SOC', 48.2567599,14.3633557),
            
            -- Stoppoints for Line 3, 4 (which have not been added yet)
            ('Landgutstraße', 'LGS', 48.3111161,14.2785268),
            ('Mühlkreisbahnhof', 'MKB', 48.3116828,14.2798383),
            ('Biegung', 'Bie', 48.3123719,14.2815446),
            ('Untergaumberg', 'UGB', 48.2794458,14.28047),
            ('Gaumberg', 'GaB', 48.2779952,14.2774228),
            ('Larnhauserweg', 'LHW', 48.2746944,14.2738397),
            ('Haag', 'Haa', 48.2708517,14.2699706),
            ('Poststraße', 'PoS', 48.268287,14.2674954),
            ('Meixnerkreuzung', 'MeiK', 48.262881,14.2602161),
            ('Harterfeldsiedlung', 'HFS', 48.259224,14.256027),
            ('Doblerholz', 'DoH', 48.2569518,14.2528532),
            ('Leonding Remise', 'LeR', 48.2550878,14.2491863),
            ('Im Bäckerfeld', 'IBae', 48.2503099,14.2447471),
            ('Langholzfeld', 'LHF', 48.248016,14.2422081),
            ('PlusCity', 'PuC', 48.242935,14.2376245),
            ('Wagram', 'Waa', 48.2377404,14.2354892),
            ('Trauner Kreuzung', 'TaK', 48.2339952,14.2359412),
            ('Trauner Kreuzung (P&R)', 'TaKP+R', 48.2334618,14.2343787),
            ('Mitterfeldstraße', 'MFS', 48.2266637,14.235882),
            ('Traun Hauptplatz', 'THP', 48.2220501,14.2379376),
            ('Schloss Traun', 'SoT', 48.2187594,14.23882),
            
            -- Rail replacement service station
            ('Sonnensteinstraße', 'SoS', 48.3124057,14.2839539);
        -- ON CONFLICT DO NOTHING;

-- Insert sample data into routestoppoint table
        INSERT INTO routestoppoint (route_id, stop_point_id, arrival_time, departure_time, order_number) VALUES
            -- Line 1-1
            (1, 1, '2024-01-01 05:21:00', '2024-01-01 05:21:00', 1), -- JKU | Universität
            (1, 2, '2024-01-01 04:22:00', '2024-01-01 04:22:00', 2), -- Schumpeterstr.
            (1, 3, '2024-01-01 04:23:00', '2024-01-01 04:23:00', 3), -- Dornach
            (1, 4, '2024-01-01 04:24:00', '2024-01-01 04:24:00', 4), -- Glaserstr.
            (1, 5, '2024-01-01 04:25:00', '2024-01-01 04:25:00', 5), -- St. Magdalena
            (1, 6, '2024-01-01 04:26:00', '2024-01-01 04:26:00', 6), -- Ferdinand-Markl-str.
            (1, 7, '2024-01-01 04:27:00', '2024-01-01 04:27:00', 7), -- Gründberg
            (1, 8, '2024-01-01 04:28:00', '2024-01-01 04:28:00', 8), -- Harbachsiedlung
            (1, 9, '2024-01-01 04:29:00', '2024-01-01 04:29:00', 9), -- Harbach
            (1, 10, '2024-01-01 04:30:00', '2024-01-01 04:30:00', 10), -- Ontlstr.
            (1, 11, '2024-01-01 04:31:00', '2024-01-01 04:31:00', 11), -- Linke Brückenstr.
            -- Gap for Freihuemerstr.  
            (1, 13, '2024-01-01 04:33:00', '2024-01-01 04:33:00', 12), -- Peuerbachstr.
            (1, 14, '2024-01-01 04:34:00', '2024-01-01 04:34:00', 13), -- Wildbergstr.
            (1, 15, '2024-01-01 04:35:00', '2024-01-01 04:35:00', 14), -- Rudolfstr.
            (1, 16, '2024-01-01 04:37:00', '2024-01-01 04:37:00', 15), -- Hauptplatz
            (1, 17, '2024-01-01 04:38:00', '2024-01-01 04:38:00', 16), -- Taubenmarkt
            (1, 18, '2024-01-01 04:40:00', '2024-01-01 04:40:00', 17), -- Mozartkreuzung
            (1, 19, '2024-01-01 04:41:00', '2024-01-01 04:41:00', 18), -- Bürgerstr.
            (1, 20, '2024-01-01 04:43:00', '2024-01-01 04:43:00', 19),-- Goethekreuzung
            (1, 21, '2024-01-01 04:44:00', '2024-01-01 04:44:00', 20), -- Hauptbahnhof
            (1, 22, '2024-01-01 04:45:00', '2024-01-01 04:45:00', 21), -- Unionkreuzung
            (1, 23, '2024-01-01 04:46:00', '2024-01-01 04:46:00', 22), -- Herz-Jesu-Kirche
            (1, 24, '2024-01-01 04:47:00', '2024-01-01 04:47:00', 23), -- Bulgariplatz
            (1, 25, '2024-01-01 04:49:00', '2024-01-01 04:49:00', 24), -- WIFI/LINZ AG
            (1, 26, '2024-01-01 04:50:00', '2024-01-01 04:50:00', 25), -- Turmstr.
            (1, 27, '2024-01-01 04:51:00', '2024-01-01 04:51:00', 26), -- Neue Welt
            (1, 28, '2024-01-01 04:52:00', '2024-01-01 04:52:00', 27), -- Scharlinz
            (1, 29, '2024-01-01 04:53:00', '2024-01-01 04:53:00', 28), -- Wahringerstr.
            (1, 30, '2024-01-01 04:54:00', '2024-01-01 04:54:00', 29), -- Wimmerstr.
            (1, 31, '2024-01-01 04:55:00', '2024-01-01 04:55:00', 30), -- Remise Kleinmünchen
            (1, 32, '2024-01-01 04:56:00', '2024-01-01 04:56:00', 31), -- Simonystr.
            (1, 33, '2024-01-01 04:57:00', '2024-01-01 04:57:00', 32), -- Dürerstr.
            (1, 34, '2024-01-01 04:58:00', '2024-01-01 04:58:00', 33), -- Dauphinestr.
            (1, 35, '2024-01-01 04:59:00', '2024-01-01 04:59:00', 34), -- Rädlerweg
            (1, 36, '2024-01-01 05:00:00', '2024-01-01 05:00:00', 35), -- Auwiesen

            -- Line 1-2
            (2, 36, '2024-01-01 05:25:00', '2024-01-01 05:25:00', 1), -- Auwiesen
            (2, 35, '2024-01-01 05:25:00', '2024-01-01 05:25:00', 2), -- Rädlerweg
            (2, 34, '2024-01-01 05:26:00', '2024-01-01 05:26:00', 3), -- Dauphinestr.
            (2, 33, '2024-01-01 05:27:00', '2024-01-01 05:27:00', 4), -- Dürerstr.
            (2, 32, '2024-01-01 05:29:00', '2024-01-01 05:29:00', 5), -- Simonystr.
            (2, 31, '2024-01-01 05:30:00', '2024-01-01 05:30:00', 6), -- Remise Kleinmünchen
            (2, 30, '2024-01-01 05:31:00', '2024-01-01 05:31:00', 7), -- Wimmerstr.
            (2, 29, '2024-01-01 05:32:00', '2024-01-01 05:32:00', 8), -- Wahringerstr.
            (2, 28, '2024-01-01 05:33:00', '2024-01-01 05:33:00', 9), -- Scharlinz
            (2, 27, '2024-01-01 05:34:00', '2024-01-01 05:34:00', 10), -- Neue Welt
            (2, 26, '2024-01-01 05:35:00', '2024-01-01 05:35:00', 11), -- Turmstr.
            (2, 25, '2024-01-01 05:36:00', '2024-01-01 05:36:00', 12), -- WIFI/LINZ AG
            (2, 24, '2024-01-01 05:38:00', '2024-01-01 05:38:00', 13), -- Bulgariplatz
            (2, 23, '2024-01-01 05:39:00', '2024-01-01 05:39:00', 14), -- Herz-Jesu-Kirche
            (2, 22, '2024-01-01 05:40:00', '2024-01-01 05:40:00', 15), -- Unionkreuzung
            (2, 21, '2024-01-01 05:41:00', '2024-01-01 05:41:00', 16), -- Hauptbahnhof
            (2, 20, '2024-01-01 05:44:00', '2024-01-01 05:44:00', 17), -- Goethekreuzung
            (2, 19, '2024-01-01 05:45:00', '2024-01-01 05:45:00', 18), -- Bürgerstr.
            (2, 18, '2024-01-01 05:47:00', '2024-01-01 05:47:00', 19), -- Mozartkreuzung
            (2, 17, '2024-01-01 05:47:00', '2024-01-01 05:47:00', 20), -- Taubenmarkt
            (2, 16, '2024-01-01 05:48:00', '2024-01-01 05:48:00', 21), -- Hauptplatz
            (2, 15, '2024-01-01 05:50:00', '2024-01-01 05:50:00', 22), -- Rudolfstr.
            (2, 14, '2024-01-01 05:51:00', '2024-01-01 05:51:00', 23), -- Wildbergstr.
            (2, 13, '2024-01-01 05:52:00', '2024-01-01 05:52:00', 24), -- Peuerbachstr.
            -- Gap for Freihuemerstr.  
            (2, 11, '2024-01-01 05:54:00', '2024-01-01 05:54:00', 25), -- Linke Brückenstr.
            (2, 10, '2024-01-01 05:55:00', '2024-01-01 05:55:00', 26), -- Ontlstr.
            (2, 9, '2024-01-01 05:56:00', '2024-01-01 05:56:00', 27), -- Harbach
            (2, 8, '2024-01-01 05:57:00', '2024-01-01 05:57:00', 28), -- Harbachsiedlung
            (2, 7, '2024-01-01 05:58:00', '2024-01-01 05:58:00', 29), -- Gründberg
            (2, 6, '2024-01-01 05:59:00', '2024-01-01 05:59:00', 30), -- Ferdinand-Markl-str.
            (2, 5, '2024-01-01 06:00:00', '2024-01-01 06:00:00', 31), -- St. Magdalena
            (2, 4, '2024-01-01 06:01:00', '2024-01-01 06:01:00', 32), -- Glaserstr.
            (2, 3, '2024-01-01 06:02:00', '2024-01-01 06:02:00', 33), -- Dornach
            (2, 2, '2024-01-01 06:03:00', '2024-01-01 06:03:00', 34), -- Schumpeterstr.
            (2, 1, '2024-01-01 06:04:00', '2024-01-01 06:04:00', 35), -- JKU | Universität

            -- Line 2-1
            (3, 1, '2024-01-01 05:30:00', '2024-01-01 05:30:00', 1), -- JKU | Universität
            (3, 2, '2024-01-01 05:30:00', '2024-01-01 05:30:00', 2), -- Schumpeterstr.
            (3, 3, '2024-01-01 05:32:00', '2024-01-01 05:32:00', 3), -- Dornach
            (3, 4, '2024-01-01 05:33:00', '2024-01-01 05:33:00', 4), -- Glaserstr.
            (3, 5, '2024-01-01 05:34:00', '2024-01-01 05:34:00', 5), -- St. Magdalena
            (3, 7, '2024-01-01 05:36:00', '2024-01-01 05:36:00', 6), -- Gründberg
            (3, 9, '2024-01-01 05:38:00', '2024-01-01 05:38:00', 7), -- Harbach
            (3, 10, '2024-01-01 05:39:00', '2024-01-01 05:39:00', 8), -- Ontlstr.
            (3, 11, '2024-01-01 05:40:00', '2024-01-01 05:40:00', 9), -- Linke Brückenstr.
            (3, 13, '2024-01-01 05:42:00', '2024-01-01 05:42:00', 10), -- Peuerbachstr.
            (3, 14, '2024-01-01 05:43:00', '2024-01-01 05:43:00', 11), -- Wildbergstr.
            (3, 15, '2024-01-01 05:44:00', '2024-01-01 05:44:00', 12), -- Rudolfstr.
            (3, 16, '2024-01-01 05:46:00', '2024-01-01 05:46:00', 13), -- Hauptplatz
            (3, 17, '2024-01-01 05:47:00', '2024-01-01 05:47:00', 14), -- Taubenmarkt
            (3, 18, '2024-01-01 05:49:00', '2024-01-01 05:49:00', 15), -- Mozartkreuzung
            (3, 19, '2024-01-01 05:50:00', '2024-01-01 05:50:00', 16), -- Bürgerstr.
            (3, 20, '2024-01-01 05:52:00', '2024-01-01 05:52:00', 17), -- Goethekreuzung
            (3, 21, '2024-01-01 05:53:00', '2024-01-01 05:53:00', 18), -- Hauptbahnhof
            (3, 22, '2024-01-01 05:54:00', '2024-01-01 05:54:00', 19), -- Unionkreuzung
            (3, 23, '2024-01-01 05:55:00', '2024-01-01 05:55:00', 20), -- Herz-Jesu-Kirche
            (3, 24, '2024-01-01 05:56:00', '2024-01-01 05:56:00', 21), -- Bulgariplatz
            (3, 25, '2024-01-01 05:58:00', '2024-01-01 05:58:00', 22), -- WIFI/LINZ AG
            (3, 26, '2024-01-01 05:59:00', '2024-01-01 05:59:00', 23), -- Turmstr.
            (3, 27, '2024-01-01 06:00:00', '2024-01-01 06:00:00', 24), -- Neue Welt
            (3, 28, '2024-01-01 06:01:00', '2024-01-01 06:01:00', 25), -- Scharlinz
            (3, 29, '2024-01-01 06:02:00', '2024-01-01 06:02:00', 26), -- Wahringerstr.
            (3, 30, '2024-01-01 06:03:00', '2024-01-01 06:03:00', 27), -- Wimmerstr.
            (3, 31, '2024-01-01 06:04:00', '2024-01-01 06:04:00', 28), -- Remise Kleinmünchen
            (3, 32, '2024-01-01 06:05:00', '2024-01-01 06:05:00', 29), -- Simonystr.
            (3, 37, '2024-01-01 06:07:00', '2024-01-01 06:07:00', 30), -- Saporoshjestr.
            (3, 38, '2024-01-01 04:58:00', '2024-01-01 06:08:00', 31), -- Ebelsberg
            (3, 39, '2024-01-01 04:59:00', '2024-01-01 06:10:00', 32), -- Wambacher Straße
            (3, 40, '2024-01-01 05:00:00', '2024-01-01 06:11:00', 33), -- Edmund-Aigner-Straße
            (3, 41, '2024-01-01 05:00:00', '2024-01-01 06:12:00', 34), -- Hartheimerstraße
            (3, 42, '2024-01-01 05:00:00', '2024-01-01 06:13:00', 35), -- Ennsfeld
            (3, 43, '2024-01-01 05:00:00', '2024-01-01 06:15:00', 36), -- Hillerstraße
            (3, 44, '2024-01-01 05:00:00', '2024-01-01 06:17:00', 37), -- Ebelsberg Bahnhof
            (3, 45, '2024-01-01 05:00:00', '2024-01-01 06:18:00', 48), -- Neufelderstraße
            (3, 46, '2024-01-01 05:00:00', '2024-01-01 06:19:00', 39), -- solarCity-Zentrum
            (3, 47, '2024-01-01 05:00:00', '2024-01-01 06:20:00', 40), -- solarCity

            -- Line 2-2
            (4, 1, '2024-01-01 06:14:00', '2024-01-01 06:14:00', 40), -- JKU | Universität
            (4, 2, '2024-01-01 06:33:00', '2024-01-01 06:33:00', 39), -- Schumpeterstr.
            (4, 3, '2024-01-01 06:12:00', '2024-01-01 06:12:00', 38), -- Dornach
            (4, 4, '2024-01-01 06:11:00', '2024-01-01 06:11:00', 37), -- Glaserstr.
            (4, 5, '2024-01-01 06:10:00', '2024-01-01 06:10:00', 36), -- St. Magdalena
            (4, 7, '2024-01-01 06:08:00', '2024-01-01 06:08:00', 35), -- Gründberg
            (4, 9, '2024-01-01 06:06:00', '2024-01-01 06:06:00', 34), -- Harbach
            (4, 10, '2024-01-01 06:05:00', '2024-01-01 06:05:00', 33), -- Ontlstr.
            (4, 11, '2024-01-01 06:04:00', '2024-01-01 06:04:00', 32), -- Linke Brückenstr.
            (4, 13, '2024-01-01 06:02:00', '2024-01-01 06:02:00', 31), -- Peuerbachstr.
            (4, 14, '2024-01-01 06:01:00', '2024-01-01 06:01:00', 30), -- Wildbergstr.
            (4, 15, '2024-01-01 06:00:00', '2024-01-01 06:00:00', 29), -- Rudolfstr.
            (4, 16, '2024-01-01 05:58:00', '2024-01-01 05:58:00', 28), -- Hauptplatz
            (4, 17, '2024-01-01 05:57:00', '2024-01-01 05:57:00', 27), -- Taubenmarkt
            (4, 18, '2024-01-01 05:55:00', '2024-01-01 05:55:00', 26), -- Mozartkreuzung
            (4, 19, '2024-01-01 05:54:00', '2024-01-01 05:54:00', 25), -- Bürgerstr.
            (4, 20, '2024-01-01 05:53:00', '2024-01-01 05:53:00', 24), -- Goethekreuzung
            (4, 21, '2024-01-01 05:51:00', '2024-01-01 05:51:00', 23), -- Hauptbahnhof
            (4, 22, '2024-01-01 05:49:00', '2024-01-01 05:49:00', 22), -- Unionkreuzung
            (4, 23, '2024-01-01 05:48:00', '2024-01-01 05:48:00', 21), -- Herz-Jesu-Kirche
            (4, 24, '2024-01-01 05:47:00', '2024-01-01 05:47:00', 20), -- Bulgariplatz
            (4, 25, '2024-01-01 05:44:00', '2024-01-01 05:44:00', 19), -- WIFI/LINZ AG
            (4, 26, '2024-01-01 05:43:00', '2024-01-01 05:43:00', 18), -- Turmstr.
            (4, 27, '2024-01-01 05:44:00', '2024-01-01 05:44:00', 17), -- Neue Welt
            (4, 28, '2024-01-01 05:42:00', '2024-01-01 05:42:00', 16), -- Scharlinz
            (4, 29, '2024-01-01 05:41:00', '2024-01-01 05:41:00', 15), -- Wahringerstr.
            (4, 30, '2024-01-01 05:40:00', '2024-01-01 05:40:00', 14), -- Wimmerstr.
            (4, 31, '2024-01-01 05:39:00', '2024-01-01 05:39:00', 13), -- Remise Kleinmünchen
            (4, 32, '2024-01-01 05:37:00', '2024-01-01 05:37:00', 12), -- Simonystr.
            (4, 37, '2024-01-01 05:35:00', '2024-01-01 05:35:00', 11), -- Saporoshjestr.
            (4, 38, '2024-01-01 05:34:00', '2024-01-01 05:34:00', 10), -- Ebelsberg
            -- Hauderweg missing
            (4, 39, '2024-01-01 05:32:00', '2024-01-01 05:32:00', 9), -- Wambacher Straße
            (4, 40, '2024-01-01 05:31:00', '2024-01-01 05:31:00', 8), -- Edmund-Aigner-Straße
            (4, 41, '2024-01-01 05:30:00', '2024-01-01 05:30:00', 7), -- Hartheimerstraße
            (4, 42, '2024-01-01 05:29:00', '2024-01-01 05:29:00', 6), -- Ennsfeld
            (4, 43, '2024-01-01 05:28:00', '2024-01-01 05:28:00', 5), -- Hillerstraße
            (4, 44, '2024-01-01 05:27:00', '2024-01-01 05:27:00', 4), -- Ebelsberg Bahnhof
            (4, 45, '2024-01-01 05:25:00', '2024-01-01 05:25:00', 3), -- Neufelderstraße
            (4, 46, '2024-01-01 05:23:00', '2024-01-01 05:23:00', 2), -- solarCity-Zentrum
            (4, 47, '2024-01-01 05:21:00', '2024-01-01 05:21:00', 1) -- solarCity
                                                                                                              
--             (1, 1, '2024-01-01 08:00:00', '2024-01-01 08:05:00', 1),
--             (1, 2, '2024-01-01 08:15:00', '2024-01-01 08:20:00', 2),
--             (2, 3, '2024-03-01 09:00:00', '2024-03-01 09:05:00', 1),
--             (2, 4, '2024-03-01 09:15:00', '2024-03-01 09:20:00', 2)
        ON CONFLICT DO NOTHING;

-- Insert sample data into trip table
        INSERT INTO trip (route_id, vehicle_id) VALUES
            (1, 101),
            (2, 102)
        ON CONFLICT DO NOTHING;

-- Insert sample data into tripcheckin table
        INSERT INTO tripcheckin (trip_id, stop_point_id, checkin_time) VALUES
            (1, 1, '2024-01-01 08:03:00'),
            (1, 2, '2024-01-01 08:13:00'),
            (2, 3, '2024-03-01 09:02:00')
        ON CONFLICT DO NOTHING;


    END $$;
