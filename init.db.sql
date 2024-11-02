-- Check if the database objects exist before creating them
DO $$
    BEGIN
        
        -- Create enumeration for holiday type
        CREATE TYPE holiday_type AS ENUM ('BankHoliday', 'SchoolVacation', 'NationalHoliday', 'ReligiousHoliday', 'Other');
        
        
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
            ('Green', '2024-01-01', '2025-12-31', 62),  -- 62 (0b0111110) für Montag bis Freitag
            ('Blue', '2024-03-01', '2025-09-30', 85),   -- 85 (0b1010101) für Mo, Mi, Fr, So
            ('Red', '2024-08-08', '2025-11-05', 85)     -- 85 (0b1010101) für Mo, Mi, Fr, So
        ON CONFLICT DO NOTHING;

-- Insert sample data into stoppoint table
        INSERT INTO stoppoint (name, short_name, latitude, longitude) VALUES
            ('Hauptbahnhof', 'Hbf', 40.748817, -73.985428),
            ('Uferpromenade', 'UPN', 34.052235, -118.243683),
            ('Ostpark', 'OPK', 51.507351, -0.127758),
            ('Industriezeile', 'IDZ', 40.712776, -74.005974)
        ON CONFLICT DO NOTHING;

-- Insert sample data into routestoppoint table
        INSERT INTO routestoppoint (route_id, stop_point_id, arrival_time, departure_time, order_number) VALUES
            (1, 1, '2024-01-01 08:00:00', '2024-01-01 08:05:00', 1),
            (1, 2, '2024-01-01 08:15:00', '2024-01-01 08:20:00', 2),
            (2, 3, '2024-03-01 09:00:00', '2024-03-01 09:05:00', 1),
            (2, 4, '2024-03-01 09:15:00', '2024-03-01 09:20:00', 2)
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
