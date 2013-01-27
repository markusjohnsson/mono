
JSIL.CultureInfo = JSIL.CultureInfo || {};

JSIL.CultureInfo.NUM_DAYS = 7;
JSIL.CultureInfo.NUM_MONTHS = 13;
JSIL.CultureInfo.NUM_SHORT_DATE_PATTERNS = 14;
JSIL.CultureInfo.NUM_LONG_DATE_PATTERNS = 8;
JSIL.CultureInfo.NUM_SHORT_TIME_PATTERNS = 11;
JSIL.CultureInfo.NUM_LONG_TIME_PATTERNS = 10;

function strcmp ( str1, str2 ) {
    return ( ( str1 == str2 ) ? 0 : ( ( str1 > str2 ) ? 1 : -1 ) );
}

function bsearch(key, base, num, comparator) {
    if (base.length === 0)
        return null;

    var min = 0;
    var max = base.length - 1;

    var comparisons = 0;
    while (comparisons < base.length) {

        var mid = min + Math.floor((max - min) / 2);

        if (mid < 0 || mid > base.length)
            return null;

        var elem = base[mid];
        var c = comparator(key, elem);

        if (c === 0)
            return elem;

        if (c > 0)
            min = mid + 1;
        else
            max = mid - 1;

        comparisons++;
    }
    return null;
};

function idx2string(idx) {
    return JSIL.Metadata.locale_strings[idx];
};

function culture_name_locator (key, elem) {
    return strcmp(key, idx2string(elem[CultureInfoNameEntry.name])); 
}

CultureInfoNameEntry = { name: 0, culture_entry_index: 1 };
CultureInfoEntry = {
    lcid: 0, parent_lcid: 1, specific_lcid: 2, region_entry_index: 3,
    name: 4, icu_name: 5, englishname: 6, displayname: 7, nativename: 8, win3lang: 9, iso3lang: 10, iso2lang: 11, territory: 12,
    calendar_data: 13,
    datetime_format_index: 14, number_format_index: 15,
    text_info: 16
};
DateTimeFormatEntry = {
	full_date_time_pattern: 0,
	long_date_pattern: 1,
	short_date_pattern: 2,
	long_time_pattern: 3,
	short_time_pattern: 4,
	year_month_pattern: 5,
	month_day_pattern: 6,

	am_designator: 7,
	pm_designator: 8,

	day_names: 9, 
	abbreviated_day_names: 10,
	month_names: 11,
	abbreviated_month_names: 12,

	calendar_week_rule: 13,
	first_day_of_week: 14,

	date_separator: 15,
	time_separator: 16,

	short_date_patterns: 17,
	long_date_patterns: 18,
	short_time_patterns: 19,
	long_time_patterns: 20
};

JSIL.CultureInfo.get_current_locale_name = function () {
    var l_lang;
    if (navigator.userLanguage) // Explorer
        l_lang = navigator.userLanguage;
    else if (navigator.language) // FF
        l_lang = navigator.language;
    else
        l_lang = "en";

    return l_lang.toLowerCase();
};

JSIL.CultureInfo.construct_internal_locale_from_current_locale = function (/*CultureInfo*/ci) {

    var locale = JSIL.CultureInfo.get_current_locale_name();
    if (locale == null)
        return false;

    ret = JSIL.CultureInfo.construct_culture_from_specific_name (ci, locale);
    
    ci.is_read_only = true;
    ci.use_user_override = true;

    return ret;
};

JSIL.CultureInfo.construct_culture_from_specific_name = function (ci, name) {

    var ne = bsearch(name, JSIL.Metadata.culture_name_entries, JSIL.Metadata.NumCultureEntries, culture_name_locator);

    if (ne == null)
        return false;

    var entry = JSIL.Metadata.culture_entries[ne[CultureInfoNameEntry.culture_entry_index]];

    /* try avoiding another lookup, often the culture is its own specific culture */
    if (entry[CultureInfoEntry.lcid] != entry[CultureInfoEntry.specific_lcid])
        entry = JSIL.CultureInfo.culture_info_entry_from_lcid(entry[CultureInfoEntry.specific_lcid]);

    if (entry)
        return JSIL.CultureInfo.construct_culture(ci, entry);
    else
        return false;
};

JSIL.CultureInfo.culture_info_entry_from_lcid = function (lcid) {
    
    ci = bsearch(lcid, JSIL.Metadata.culture_entries, JSIL.Metadata.NumCultureEntries, function (key, elem) { return key - elem[CultureInfoEntry.lcid]; });
    return ci;
};

JSIL.CultureInfo.construct_culture = function(target, /*CultureInfoEntry*/ ci) {

    target.cultureID = ci[CultureInfoEntry.lcid];
    target.m_name = idx2string(ci[CultureInfoEntry.name]);
    target.icu_name = idx2string(ci[CultureInfoEntry.icu_name]);
    target.displayname = idx2string(ci[CultureInfoEntry.displayname]);
    target.englishname = idx2string(ci[CultureInfoEntry.englishname]);
    target.nativename = idx2string(ci[CultureInfoEntry.nativename]);
    target.win3lang = idx2string(ci[CultureInfoEntry.win3lang]);
    target.iso3lang = idx2string(ci[CultureInfoEntry.iso3lang]);
    target.iso2lang = idx2string(ci[CultureInfoEntry.iso2lang]);
    target.territory = idx2string(ci[CultureInfoEntry.territory]);
    target.parent_lcid = ci[CultureInfoEntry.parent_lcid];
    target.specific_lcid = ci[CultureInfoEntry.specific_lcid];
    target.datetime_index = ci[CultureInfoEntry.datetime_format_index];
    target.number_index = ci[CultureInfoEntry.number_format_index];
    target.calendar_data = ci[CultureInfoEntry.calendar_data];
    target.textinfo_data = ci[CultureInfoEntry.text_info];
    
    return true;
}

JSIL.CultureInfo.construct_internal_locale_from_name = function (self, name) {

    var ne = bsearch(name, JSIL.Metadata.culture_name_entries, JSIL.Metadata.NumCultureEntries, culture_name_locator);

    if (ne === null) {
        return false;
    }

    return JSIL.CultureInfo.construct_culture(self, JSIL.Metadata.culture_entries[ne[CultureInfoNameEntry.culture_entry_index]]);
};

JSIL.CultureInfo.construct_internal_locale_from_specific_name = function (ci, name) {
    return JSIL.CultureInfo.construct_culture_from_specific_name(ci, name);
};

JSIL.CultureInfo.construct_internal_locale_from_lcid = function (self, lcid) {
    var ci = JSIL.CultureInfo.culture_info_entry_from_lcid(lcid);

    if (ci == null)
        return false;

    return construct_culture(self, ci);
};

JSIL.CultureInfo.construct_datetime_format = function ( /*CultureInfo*/ self) {


    var datetime = self.dateTimeInfo;
	var dfe = JSIL.Metadata.datetime_format_entries [self.datetime_index];

	datetime.readOnly = self.is_read_only;
	datetime.AbbreviatedDayNames = JSIL.CultureInfo.create_names_array_idx (dfe[DateTimeFormatEntry.abbreviated_day_names], JSIL.CultureInfo.NUM_DAYS);
	datetime.AbbreviatedMonthNames = JSIL.CultureInfo.create_names_array_idx (dfe[DateTimeFormatEntry.abbreviated_month_names], JSIL.CultureInfo.NUM_MONTHS);
	datetime.AMDesignator = idx2string (dfe[DateTimeFormatEntry.am_designator]);
	datetime.CalendarWeekRule = dfe[DateTimeFormatEntry.calendar_week_rule];
	datetime.DateSeparator = idx2string (dfe[DateTimeFormatEntry.date_separator]);
	datetime.DayNames = JSIL.CultureInfo.create_names_array_idx (dfe[DateTimeFormatEntry.day_names], JSIL.CultureInfo.NUM_DAYS);
	datetime.FirstDayOfWeek = dfe[DateTimeFormatEntry.first_day_of_week];
	datetime.FullDateTimePattern = idx2string (dfe[DateTimeFormatEntry.full_date_time_pattern]);
	datetime.LongDatePattern = idx2string (dfe[DateTimeFormatEntry.long_date_pattern]);
	datetime.LongTimePattern = idx2string (dfe[DateTimeFormatEntry.long_time_pattern]);
	datetime.MonthDayPattern = idx2string (dfe[DateTimeFormatEntry.month_day_pattern]);
	datetime.MonthNames = JSIL.CultureInfo.create_names_array_idx (dfe[DateTimeFormatEntry.month_names], JSIL.CultureInfo.NUM_MONTHS);
	datetime.PMDesignator = idx2string (dfe[DateTimeFormatEntry.pm_designator]);
	datetime.ShortDatePattern = idx2string (dfe[DateTimeFormatEntry.short_date_pattern]);
	datetime.ShortTimePattern = idx2string (dfe[DateTimeFormatEntry.short_time_pattern]);
	datetime.TimeSeparator = idx2string (dfe[DateTimeFormatEntry.time_separator]);
	datetime.YearMonthPattern = idx2string (dfe[DateTimeFormatEntry.year_month_pattern]);
	datetime.ShortDatePatterns = JSIL.CultureInfo.create_names_array_idx (dfe[DateTimeFormatEntry.short_date_patterns],
			JSIL.CultureInfo.NUM_SHORT_DATE_PATTERNS);
	datetime.LongDatePatterns = JSIL.CultureInfo.create_names_array_idx (dfe[DateTimeFormatEntry.long_date_patterns],
			JSIL.CultureInfo.NUM_LONG_DATE_PATTERNS);
	datetime.ShortTimePatterns = JSIL.CultureInfo.create_names_array_idx (dfe[DateTimeFormatEntry.short_time_patterns],
			JSIL.CultureInfo.NUM_SHORT_TIME_PATTERNS);
	datetime.LongTimePatterns = JSIL.CultureInfo.create_names_array_idx (dfe[DateTimeFormatEntry.long_time_patterns],
			JSIL.CultureInfo.NUM_LONG_TIME_PATTERNS);
};

JSIL.CultureInfo.create_names_array_idx = function(names, ml)
{
	var i, len = 0;

	if (names == null)
		return null;

	for (i = 0; i < ml; i++) {
		if (names [i] == 0)
			break;
		len++;
	}

	ret = new Array(len); //mono_array_new_cached (mono_domain_get (), mono_get_string_class (), len);

	for(i = 0; i < len; i++)
		ret[i] = idx2string (names [i]);

	return ret;
}