# 0003. Separate Feature Logic from Forms

<!--
How to use this template

- Copy this file to adr/NNNN-short-slug.md: the next number, plus two or three
  words. The full title goes in the heading above, not the file name.
- Name the decision, not the problem, in words that stay true once it is old:
  "Use the OpenXML SDK to Sort Doubleknot Rosters", not "Reimplement sorting".
- Write the sections in order. Context first, without the solution.
- Answer the questions in each comment, then delete the comment. Comments are
  hidden in rendered views, so any you miss do no harm.
- Add a row for the ADR to section 9.8 of the architecture document.
-->

**Status:** Proposed

**Date:** 2026-11-07

<!--
Status: Proposed while being written; Accepted once decided. Add
"**Implemented:** YYYY-MM-DD" below Date when Done When is complete. If a later
ADR replaces this one, set Status to "Superseded by ADR NNNN", and don't
rewrite the rest.
Date: the date of the decision.
-->

**Addresses:** [TD4](../architecture.md#112-technical-debt).

<!--
- Which risks (R) and debt items (TD) in architecture.md section 11 does this
  fix completely? Which only in part?
- For each partial item, a few words in brackets saying which part.
- Use the stable IDs, never list numbers.
- Delete this line if the ADR addresses no listed item.
-->

**Supersedes:** Baseline 9.N, in part: which part.

<!--
- Does this replace an earlier decision, in section 9 or an earlier ADR?
  All of it, or only part? Name the decision and the part, not individual
  sentences.
- Delete this line if nothing is superseded.
-->

## Contents

- [Context](#context)
- [Decision](#decision)
- [Considered Options](#considered-options)
- [Consequences](#consequences)
- [Done When](#done-when)
- [Documentation Updates](#documentation-updates)

## Context

[↑ Contents](#contents)

<!--
Why a decision is needed. A reader should finish this able to judge the
options themselves, without knowing which you chose. Don't mention the
solution.

1. How does it work today? Where is it in the code: which class, method,
   file? Which file or data exactly (for example, the roster copy, not the
   export)?
2. What's wrong with that, and why does it need deciding now? Name the
   section 11 items it causes, with their IDs. Say when a problem happens,
   not "in some cases".
3. What does that stop or block? Be precise: "Create Paperwork cannot run
   without Excel", not "no testing is possible". Don't attribute to this
   problem things it doesn't cause.
4. What must any solution achieve? State each as a need, with its reason:
   who or what depends on it. Leave out what any correct solution does
   anyway; include what rules out an option.
5. What has to stay true afterwards? Which features rely on the current
   behavior?
6. Is every claim here something a reader could check?
-->

## Decision

[↑ Contents](#contents)

<!--
What was decided, in the present tense ("Sorting lives in...", not
"Sorting will live in..."). Group it into parts with bold labels, such as:

**Where it lives.** Which class or component, and why there.
**What it does.** Its scope, and what it leaves alone.
**Rules.** The behavior, stated so a test can check it. Main rule first,
then tie-breaks and exceptions.
**How it's used.** What changes for the code that calls it, and why it's
done that way.

For each part:
- Is the reason stated, not just the choice?
- Where it fixes a section 11 item, say so as a commitment, with the ID.
  Say only what this decision guarantees, not more.
- Does it claim more than it does? Name the specific part it changes.
- Is everything here within the ADR's purpose? If not, it's an option to
  record and defer, not part of the decision.
-->

## Considered Options

[↑ Contents](#contents)

<!--
The alternatives, and why they weren't chosen. One bullet each:

* **Option name**: What it is. What it would gain. Why it wasn't chosen.

- What else could solve the problem in the Context? Include partial
  solutions, such as hiding a dependency behind an interface.
- What does each gain? What does it cost?
- Is it rejected, or only deferred? If deferred, say where it belongs
  ("left to a later ADR").
- If it's out of scope, say what makes it too big: what else it would
  change.
- Record what you learned that a later ADR will need, so it isn't
  rediscovered.
-->

## Consequences

[↑ Contents](#contents)

<!--
What's true once this is implemented, good and bad. One point per bullet.

1. What becomes possible? What can now be run, tested, or released?
2. What stays the same, or is still unsolved? Link the remaining part of
   each partly addressed item by its ID.
3. What new costs or risks does it bring? Is anything slower, more
   complicated, or newly the code's responsibility? Is this placement or
   design final, or might a later ADR change it?
-->

## Done When

[↑ Contents](#contents)

<!--
The conditions for marking the ADR implemented. Each must be checkable.

### Tests Pass
- A test for each section 11 item fixed, with input built so the old bug
  would show. One thing per test.
- A test for each rule in the Decision.
- Edge cases: already in the right state; empty (for a roster, header
  only); a single item; boundaries such as the first and last rows.
- Anything that could break while doing the work by hand that a tool used
  to do, such as file structure.
- Checks against real data: name the script, say "run locally", and the
  settings it must match.

### Code Changed
- What's added, removed, or changed, so a reader can check it in the code.

### Documentation Updated
- The documentation updates below are made.
-->

## Documentation Updates

[↑ Contents](#contents)

<!--
What must change in architecture.md when this is implemented. One bullet
per change: the section, linked, then what changes.

* [6.1](../architecture.md#61-create-paperwork): what changes

- Search architecture.md for the terms this ADR changes (for example
  "sort" or "Excel") to find every mention, including diagrams and tables.
- Section 11: which items are removed? Do any other places refer to them,
  and would be left pointing at a retired ID?
- Superseded decisions: change the status only ("...; partly superseded by
  ADR NNNN"); don't rewrite their text.
- Section 9.8: update this ADR's row when its status changes.
- Only list what this ADR changes. Anything else you notice goes in its own
  commit or a later ADR.
-->