-- SLIM 70-PRO
-- User Manual
-- SY-HDBT-70SP-T
-- SY-HDBT-70SP-R

CONNECT("COM7", "RAW", 57600, "NONE", 1, 8, 100, 100)

SEND("GET TX HELP\r")

function OnReceive(str)
    print(str)
end

