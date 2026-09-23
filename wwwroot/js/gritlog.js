// GritLog - 껍데기용 스크립트 (가짜 데이터)
// 나중에 Ajax 붙일 때 표시한 부분만 교체하면 됨

$(function () {

    // ---------- 1. 오늘 체크 토글 (지금은 화면만 바뀜) ----------
    $(document).on('change', '.gl-check-item input', function () {
        const $item = $(this).closest('.gl-check-item');
        $item.toggleClass('done', this.checked);
        updateTodayCount();

        // TODO(Phase 3): 여기서 서버 호출
        // $.post('/Habit/ToggleCheck', { habitId: $item.data('id') })
        //   .done(res => $item.find('.gl-streak').text('🔥 ' + res.streak + '일'));
    });

    function updateTodayCount() {
        const total = $('.gl-check-item').length;
        const done = $('.gl-check-item.done').length;
        $('#todayDone').text(done);
        $('#todayTotal').text(total);
        $('#todayBar').css('width', total ? (done / total * 100) + '%' : '0%');
    }
    updateTodayCount();

    // ---------- 2. 잔디밭 ----------
    const $grid = $('#heatmapGrid');
    if ($grid.length) {
        // TODO(Phase 4): $.get('/Habit/GetHeatmapData') 결과로 교체
        renderHeatmap($grid, makeFakeData(26));
    }

    // data: [{ date: 'yyyy-MM-dd', count: n }]
    function renderHeatmap($target, data, weeks = 26) {
        const map = {};
        data.forEach(d => map[d.date] = d.count);

        const today = new Date();
        today.setHours(0, 0, 0, 0);
        const start = new Date(today);
        start.setDate(today.getDate() - today.getDay() - (weeks - 1) * 7); // 일요일 시작

        let html = '';
        let lastMonth = start.getMonth();
        let lastLabelCol = -9;
        const cur = new Date(start);

        for (let w = 0; w < weeks; w++) {
            // 그 주 토요일 기준으로 달이 바뀌면 라벨 (라벨끼리 3칸 이상 떨어뜨려 겹침 방지)
            const sat = new Date(cur); sat.setDate(cur.getDate() + 6);
            const month = sat.getMonth();
            let label = '';
            if ((w === 0 || month !== lastMonth) && w - lastLabelCol >= 3) {
                label = (month + 1) + '월';
                lastLabelCol = w;
            }
            lastMonth = month;
            html += `<span class="gl-heat-month">${label}</span>`;

            for (let d = 0; d < 7; d++) {
                const key = toKey(cur);
                const count = map[key] || 0;
                const future = cur > today;
                html += `<span class="gl-cell${future ? ' future' : ''}" data-level="${levelOf(count)}"
                               title="${key} · ${count}개 완료"></span>`;
                cur.setDate(cur.getDate() + 1);
            }
        }
        $target.html(html);
        $target.closest('.gl-heatmap-wrap').scrollLeft(9999); // 최신 주가 보이게
    }

    function levelOf(count) {
        if (count <= 0) return 0;
        if (count === 1) return 1;
        if (count === 2) return 2;
        if (count === 3) return 3;
        return 4;
    }

    function toKey(date) {
        const y = date.getFullYear();
        const m = String(date.getMonth() + 1).padStart(2, '0');
        const d = String(date.getDate()).padStart(2, '0');
        return `${y}-${m}-${d}`;
    }

    function makeFakeData(weeks) {
        const out = [];
        const d = new Date();
        for (let i = 0; i < weeks * 7; i++) {
            const r = Math.random();
            out.push({ date: toKey(d), count: r < .3 ? 0 : Math.ceil(r * 4) });
            d.setDate(d.getDate() - 1);
        }
        return out;
    }

    // ---------- 3. 등록 모달: 추천 칩 누르면 입력칸에 채우기 ----------
    $(document).on('click', '.gl-chip', function () {
        $('#habitName').val($(this).text()).trigger('focus');
    });

    // 수정 버튼 누르면 모달에 값 채우기
    $(document).on('click', '[data-edit-id]', function () {
        $('#habitModalTitle').text('습관 수정');
        $('#habitId').val($(this).data('edit-id'));
        $('#habitName').val($(this).data('edit-name'));
        $('#habitForm').attr('action', '/Habit/Edit/' + $(this).data('edit-id'));
    });
    $('#habitModal').on('hidden.bs.modal', function () {
        $('#habitModalTitle').text('새 습관 추가');
        $('#habitId').val('');
        $('#habitName').val('');
        $('#habitForm').attr('action', '/Habit/Create');
    });

    // ---------- 4. 통계 차트 ----------
    const chartEl = document.getElementById('rateChart');
    if (chartEl && window.Chart) {
        // TODO(Phase 4): $.get('/Habit/GetStats') 결과로 교체
        const stats = [
            { habitName: '런닝 30분', rate: 83 },
            { habitName: '스쿼트 50개', rate: 67 },
            { habitName: '플랭크 3분', rate: 90 },
            { habitName: '스트레칭', rate: 45 }
        ];
        new Chart(chartEl, {
            type: 'bar',
            data: {
                labels: stats.map(s => s.habitName),
                datasets: [{
                    data: stats.map(s => s.rate),
                    backgroundColor: '#2f9e4f',
                    borderRadius: 6,
                    maxBarThickness: 44
                }]
            },
            options: {
                maintainAspectRatio: false,
                plugins: { legend: { display: false } },
                scales: {
                    y: { min: 0, max: 100, ticks: { callback: v => v + '%' }, grid: { color: '#eef0ea' } },
                    x: { grid: { display: false } }
                }
            }
        });
    }
});
