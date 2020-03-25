create table duty
(
    DutyType        int         not null
        primary key,
    DutyDescription varchar(32) not null
);

create table test_result
(
    TestResultType        int         not null
        primary key,
    TestResultDescription varchar(32) not null
);

create table resident
(
    ResidentID          int auto_increment
        primary key,
    FirstName           varchar(32)       not null,
    LastName            varchar(32)       not null,
    PGY                 varchar(5)        not null,
    PhoneNumber         varchar(16)       null,
    SymptomsDate        datetime          null,
    SymptomsDescription varchar(256)      null,
    Covid19TestDate     datetime          null,
    Covid19TestResult   int     default 0 null,
    QuarantinedUntil    datetime          null,
    IsQuarantined       tinyint default 0 null,
    constraint resident_test_result_TestResultType_fk
        foreign key (Covid19TestResult) references test_result (TestResultType)
            on update cascade on delete cascade
);

create table duty_assignment
(
    ResidentID   int      not null,
    DutyType     int      not null,
    DateStart    datetime not null,
    DateEnd      datetime not null,
    DateAssigned datetime not null,
    constraint duty_assignment_duty_DutyType_fk
        foreign key (DutyType) references duty (DutyType)
            on update cascade on delete cascade,
    constraint duty_assignment_resident_ResidentID_fk
        foreign key (ResidentID) references resident (ResidentID)
            on update cascade on delete cascade
);


INSERT INTO residentlog.duty (DutyType, DutyDescription) VALUES (1, 'ICU Day');
INSERT INTO residentlog.duty (DutyType, DutyDescription) VALUES (2, 'ICU Night');
INSERT INTO residentlog.duty (DutyType, DutyDescription) VALUES (3, 'COVID Day');
INSERT INTO residentlog.duty (DutyType, DutyDescription) VALUES (4, 'COVID Night');
INSERT INTO residentlog.duty (DutyType, DutyDescription) VALUES (5, 'Floor');
INSERT INTO residentlog.duty (DutyType, DutyDescription) VALUES (6, 'Nephrology');
INSERT INTO residentlog.duty (DutyType, DutyDescription) VALUES (7, 'Cardiology');


INSERT INTO residentlog.test_result (TestResultType, TestResultDescription) VALUES (0, 'Not Tested');
INSERT INTO residentlog.test_result (TestResultType, TestResultDescription) VALUES (1, 'Pending');
INSERT INTO residentlog.test_result (TestResultType, TestResultDescription) VALUES (2, 'Negative');
INSERT INTO residentlog.test_result (TestResultType, TestResultDescription) VALUES (3, 'Positive');

