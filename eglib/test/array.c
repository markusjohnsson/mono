#include <stdio.h>
#include <string.h>
#include <glib.h>
#include "test.h"

/* example from glib documentation */
RESULT
test_array_big ()
{
	GArray *garray;
	gint i;

	/* We create a new array to store gint values.
	   We don't want it zero-terminated or cleared to 0's. */
	garray = g_array_new (FALSE, FALSE, sizeof (gint));
	for (i = 0; i < 10000; i++)
		g_array_append_val (garray, i);

	for (i = 0; i < 10000; i++)
		if (g_array_index (garray, gint, i) != i)
			return FAILED ("array value didn't match");
	
	g_array_free (garray, TRUE);

	return NULL;
}

RESULT
test_array_index ()
{
	GArray *array = g_array_new (FALSE, FALSE, sizeof (int));
	int v;

	v = 27;
	g_array_append_val (array, v);

	if (27 != g_array_index (array, int, 0))
		return FAILED ("");

	g_array_free (array, TRUE);

	return NULL;
}

RESULT
test_array_append_zero_terminated ()
{
	GArray *array = g_array_new (TRUE, FALSE, sizeof (int));
	int v;

	v = 27;
	g_array_append_val (array, v);

	if (27 != g_array_index (array, int, 0))
		return FAILED ("g_array_append_val failed");

	if (0 != g_array_index (array, int, 1))
		return FAILED ("zero_terminated didn't append a zero element");

	g_array_free (array, TRUE);

	return NULL;
}

RESULT
test_array_append ()
{
	GArray *array = g_array_new (FALSE, FALSE, sizeof (int));
	int v;

	if (0 != array->len)
		return FAILED ("initial array length not zero");

	v = 27;

	g_array_append_val (array, v);

	if (1 != array->len)
		return FAILED ("array append failed");

	g_array_free (array, TRUE);

	return NULL;
}

RESULT
test_array_append2 ()
{
	GArray *array = g_array_new (FALSE, FALSE, sizeof (gpointer));

	g_array_insert_val (array, 0, array);

	if (array != g_array_index (array, gpointer, 0))
		return FAILED ("The value in the array is incorrect");

	g_array_insert_val (array, 1, array);
	g_array_insert_val (array, 2, array);
	
	g_array_free (array, TRUE);
	return NULL;
}

RESULT
test_array_remove ()
{
	GArray *array = g_array_new (FALSE, FALSE, sizeof (int));
	int v[] = {30, 29, 28, 27, 26, 25};

	g_array_append_vals (array, v, 6);

	if (6 != array->len)
		return FAILED ("append_vals fail");

	g_array_remove_index (array, 3);

	if (5 != array->len)
		return FAILED ("remove_index failed to update length");

	if (26 != g_array_index (array, int, 3))
		return FAILED ("remove_index failed to update the array");

	g_array_free (array, TRUE);

	return NULL;
}

static Test array_tests [] = {
	{"big", test_array_big},
	{"append", test_array_append},
	{"append2", test_array_append2},
	{"index", test_array_index},
	{"remove", test_array_remove},
	{"append_zero_term", test_array_append_zero_terminated},
	{NULL, NULL}
};

DEFINE_TEST_GROUP_INIT(array_tests_init, array_tests)
